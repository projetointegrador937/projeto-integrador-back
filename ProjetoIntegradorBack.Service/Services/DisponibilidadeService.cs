using ProjetoIntegradorBack.Domain.Interfaces;
using ProjetoIntegradorBack.Shared.Pattern;

namespace ProjetoIntegradorBack.Service.Services;

public sealed class DisponibilidadeService : IDisponibilidadeService
{
    private readonly IBarberShopRepository _legacyRepository;
    private readonly IAppointmentRepository _legacyAppointments;

    public DisponibilidadeService(IBarberShopRepository legacyRepository, IAppointmentRepository legacyAppointments)
    {
        _legacyRepository = legacyRepository;
        _legacyAppointments = legacyAppointments;
    }

    public async Task<Result<IReadOnlyList<TimeOnly>>> ObterHorariosDisponiveisAsync(Guid estabelecimentoId, Guid funcionarioId, Guid servicoId, DateOnly data, CancellationToken ct = default)
    {
        var schedule = await _legacyRepository.GetBarberSchedulesAsync(estabelecimentoId, funcionarioId);
        var service = await _legacyRepository.GetServiceByIdAsync(estabelecimentoId, servicoId);
        if (schedule.IsFailure || service.IsFailure)
            return Result<IReadOnlyList<TimeOnly>>.AsFailure(new Failure(404, "Agenda ou serviço não encontrado."));

        var appointments = await _legacyAppointments.GetAppointmentsByBarberIdAsync(estabelecimentoId, funcionarioId, data);
        var booked = appointments.IsSuccess ? appointments.Success!.ToList() : [];
        var result = new List<TimeOnly>();
        var current = schedule.Success!.Start;
        while (current.AddMinutes(service.Success!.DurationInMinutes) <= schedule.Success.End)
        {
            var start = data.ToDateTime(current);
            var end = start.AddMinutes(service.Success.DurationInMinutes);
            if (schedule.Success.BreakStart.HasValue && schedule.Success.BreakEnd.HasValue &&
                start.TimeOfDay < schedule.Success.BreakEnd.Value.ToTimeSpan() && end.TimeOfDay > schedule.Success.BreakStart.Value.ToTimeSpan())
            { current = current.AddMinutes(service.Success.DurationInMinutes); continue; }
            if (!booked.Any(a => start < a.EndDateTime && end > a.StartDateTime)) result.Add(current);
            current = current.AddMinutes(service.Success.DurationInMinutes);
        }
        return Result<IReadOnlyList<TimeOnly>>.AsSuccess(result);
    }
}
