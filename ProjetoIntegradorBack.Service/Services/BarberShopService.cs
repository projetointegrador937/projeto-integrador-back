using ProjetoIntegradorBack.Domain.Interfaces;
using ProjetoIntegradorBack.Shared.Pattern;

namespace ProjetoIntegradorBack.Service.Services
{
    public class BarberShopService : IBarberShopService
    {
        private readonly IBarberShopRepository _barberShopRepository;
        private readonly IAppointmentRepository _appointmentRepository;

        public BarberShopService(IBarberShopRepository barberShopRepository, IAppointmentRepository appointmentRepository)
        {
            _barberShopRepository = barberShopRepository;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Result<IEnumerable<TimeOnly>>> GetBarberAvailableTimesAsync(
         Guid barberShopId,
         Guid barberId,
         Guid serviceId,
         DateOnly date)
        {
            try
            {
                var appointmentsResult =
                    await _appointmentRepository.GetAppointmentsByBarberIdAsync(
                        barberShopId,
                        barberId,
                        date);

                var businessHoursResult =
                    await _barberShopRepository.GetBusinessHoursAsync(barberShopId);

                var serviceResult =
                    await _barberShopRepository.GetServiceByIdAsync(barberShopId, serviceId);

                if (businessHoursResult.IsFailure)
                    return Result<IEnumerable<TimeOnly>>.AsFailure(
                        new Failure(400, "Business hours not found."));

                if (serviceResult.IsFailure)
                    return Result<IEnumerable<TimeOnly>>.AsFailure(
                        new Failure(400, "Service not found."));

                var appointments = appointmentsResult.Success
                    .OrderBy(x => x.StartDateTime)
                    .ToList();

                var businessHours = businessHoursResult.Success;
                var service = serviceResult.Success;

                var availableTimes = new List<TimeOnly>();

                var currentTime = businessHours.Start;

                while (currentTime.AddMinutes(service.DurationInMinutes) <= businessHours.End)
                {
                    var candidateStart = date.ToDateTime(currentTime);
                    var candidateEnd = candidateStart.AddMinutes(service.DurationInMinutes);

                    var hasConflict = appointments.Any(a =>
                        candidateStart < a.EndDateTime &&
                        candidateEnd > a.StartDateTime);

                    if (!hasConflict)
                    {
                        availableTimes.Add(currentTime);
                    }

                    currentTime = currentTime.AddMinutes(service.DurationInMinutes);
                }

                return Result<IEnumerable<TimeOnly>>.AsSuccess(availableTimes);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<TimeOnly>>.AsFailure(
                    new Failure(500, ex.Message));
            }
        }

    }
}
