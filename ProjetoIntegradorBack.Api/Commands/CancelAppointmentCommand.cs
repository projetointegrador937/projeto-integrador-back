using Prime.Architecture.Mediator.Standard.Requests;
namespace ProjetoIntegradorBack.Api.Commands;
public record CancelAppointmentCommand(Guid Id) : IRequest<string>;
