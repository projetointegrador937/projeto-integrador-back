using Prime.Architecture.Mediator.Standard.Requests;

namespace ProjetoIntegradorBack.Api.Commands;

public record UpdateEmployeeLinkCommand(Guid Id, Guid EstablishmentId, bool IsActive) : IRequest<string>;
