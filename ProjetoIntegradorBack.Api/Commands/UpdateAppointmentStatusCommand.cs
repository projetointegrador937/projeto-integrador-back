using ProjetoIntegradorBack.Domain.Enums;
using Prime.Architecture.Mediator.Standard.Requests;
namespace ProjetoIntegradorBack.Api.Commands;
public record UpdateAppointmentStatusCommand(Guid Id, StatusAppointment Status) : IRequest<string>;
