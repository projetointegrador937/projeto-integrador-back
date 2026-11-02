using ProjetoIntegradorBack.Domain.DTOs;
using Prime.Architecture.Mediator.Standard.Requests;
namespace ProjetoIntegradorBack.Api.Commands;
public record RescheduleAppointmentCommand(Guid Id, DateTime NewDateTime) : IRequest<AppointmentDto>;
