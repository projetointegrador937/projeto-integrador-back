using Prime.Architecture.Mediator.Standard.Requests;

namespace ProjetoIntegradorBack.Api.Commands;

public record DeactivateServiceCommand(Guid Id, Guid EstablishmentId) : IRequest<string>;
