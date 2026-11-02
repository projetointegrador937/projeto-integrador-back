using ProjetoIntegradorBack.Api.Commands;
using ProjetoIntegradorBack.Api.Queries;
using ProjetoIntegradorBack.Domain.DTOs;
using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Domain.Enums;
using ProjetoIntegradorBack.Domain.Interfaces;
using Prime.Architecture.Mapper.Interfaces;
using Prime.Architecture.Mediator.Standard.Requests;

namespace ProjetoIntegradorBack.Api.Handlers.NavalhaCorte;

public sealed class MeusAppointmentsHandler(IAppointmentRepository repository, IMapper mapper) : IRequestHandler<MeusAppointmentsQuery, PagedResultDto<AppointmentDto>>
{
    public async Task<PagedResultDto<AppointmentDto>> Handle(MeusAppointmentsQuery request, CancellationToken ct)
    {
        var list = await repository.GetPorClienteAsync(request.ClientId, request.Status, request.Page, request.Tamanho, ct);
        var total = await repository.ContarPorClienteAsync(request.ClientId, request.Status, ct);
        return new() { Data = list.IsSuccess ? list.Success!.Select(x => mapper.Map<Appointment, AppointmentDto>(x)).ToList() : [], Page = request.Page, PageSize = request.Tamanho, Total = total.IsSuccess ? total.Success : 0 };
    }
}

public sealed class CriarAppointmentNovoHandler(IAppointmentRepository repository, IServiceRepository servicos, IMapper mapper) : IRequestHandler<CreateAppointmentCommand, AppointmentDto>
{
    public async Task<AppointmentDto> Handle(CreateAppointmentCommand request, CancellationToken ct)
    {
        var service = await servicos.GetPorIdAsync(request.ServiceId, ct);
        if (service.IsFailure) throw new KeyNotFoundException("Serviço não encontrado.");
        var conflict = await repository.HasTimeConflictAsync(request.EmployeeId, request.DateTime, service.Success!.DurationMinutes, ct);
        if (conflict.IsSuccess && conflict.Success) throw new InvalidOperationException("Conflito de horário.");
        var result = await repository.AddAsync(new Appointment { ClientId = request.ClientId, EstablishmentId = request.EstablishmentId, EmployeeId = request.EmployeeId, ServiceId = request.ServiceId, DateTime = request.DateTime, Notes = request.Notes, TotalAmount = service.Success.Price, Status = StatusAppointment.Pendente }, ct);
        if (result.IsFailure) throw new InvalidOperationException(result.Failure!.ToString());
        return mapper.Map<Appointment, AppointmentDto>(result.Success!);
    }
}

public sealed class RemarcarAppointmentHandler(IAppointmentRepository repository, IMapper mapper) : IRequestHandler<RescheduleAppointmentCommand, AppointmentDto>
{
    public async Task<AppointmentDto> Handle(RescheduleAppointmentCommand request, CancellationToken ct)
    {
        var found = await repository.GetPorIdAsync(request.Id, ct); if (found.IsFailure) throw new KeyNotFoundException("Appointment não encontrado.");
        found.Success!.DateTime = request.NewDateTime; found.Success.Status = StatusAppointment.Pendente;
        var result = await repository.UpdateAsync(found.Success, ct); if (result.IsFailure) throw new InvalidOperationException(result.Failure!.ToString());
        return mapper.Map<Appointment, AppointmentDto>(result.Success!);
    }
}

public sealed class CancelarAppointmentHandler(IAppointmentRepository repository) : IRequestHandler<CancelAppointmentCommand, string>
{
    public async Task<string> Handle(CancelAppointmentCommand request, CancellationToken ct)
    { var found = await repository.GetPorIdAsync(request.Id, ct); if (found.IsFailure) throw new KeyNotFoundException("Appointment não encontrado."); found.Success!.Status = StatusAppointment.Cancelado; var result = await repository.UpdateAsync(found.Success, ct); if (result.IsFailure) throw new InvalidOperationException(result.Failure!.ToString()); return "Appointment cancelado com sucesso."; }
}

public sealed class AlterarStatusAppointmentHandler(IAppointmentRepository repository) : IRequestHandler<UpdateAppointmentStatusCommand, string>
{
    public async Task<string> Handle(UpdateAppointmentStatusCommand request, CancellationToken ct)
    { var found = await repository.GetPorIdAsync(request.Id, ct); if (found.IsFailure) throw new KeyNotFoundException("Appointment não encontrado."); found.Success!.Status = request.Status; var result = await repository.UpdateAsync(found.Success, ct); if (result.IsFailure) throw new InvalidOperationException(result.Failure!.ToString()); return "Status atualizado com sucesso."; }
}
