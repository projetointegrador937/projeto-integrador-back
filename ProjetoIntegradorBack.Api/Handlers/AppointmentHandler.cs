using ProjetoIntegradorBack.Api.Commands;
using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Domain.Interfaces;
using ProjetoIntegradorBack.Shared.Pattern;

namespace ProjetoIntegradorBack.Api.Handlers
{
    public class AppointmentHandler
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IBarberShopService _barberShopService;
        private readonly IBarberShopRepository _barberShopRepository;

        public AppointmentHandler(IAppointmentRepository appointmentRepository, IBarberShopService barberShopService, IBarberShopRepository barberShopRepository)
        {
            _appointmentRepository = appointmentRepository;
            _barberShopService = barberShopService;
            _barberShopRepository = barberShopRepository;
        }

        public async Task<Result<IEnumerable<TimeOnly>>> HandleGetBarberAvailableTimesAsync(Guid barberShopId, Guid barberId, Guid serviceId, DateOnly date)
        {
            var result = await _barberShopService.GetBarberAvailableTimesAsync(barberShopId, barberId, serviceId, date);
            return result;
        }

        public async Task<Result<string>> HandleCreateAppointmentAsync(CreateAppointmentCommand command)
        {
            var serviceTime = await _barberShopRepository.GetServiceByIdAsync(command.BarberShopId, command.ServiceId);
            var dateTime = command.Date.ToDateTime(command.Time);
            var endtime = dateTime.AddMinutes(serviceTime.Success.DurationInMinutes);
            var appointment = Appointment.Create(
                command.BarberId,
                command.ServiceId,
                command.CustomerName,
                dateTime,
                endtime
            );
            var result = await _appointmentRepository.AddAppointmentAsync(appointment);
            return result;
        }
    }
}
