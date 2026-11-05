using Prime.Architecture.Mediator.Standard.Requests;
namespace ProjetoIntegradorBack.Api.Commands;
using ProjetoIntegradorBack.Domain.DTOs;
public record CancelAppointmentCommand(Guid Id, Guid ClientId) : IRequest<AppointmentDto>;
