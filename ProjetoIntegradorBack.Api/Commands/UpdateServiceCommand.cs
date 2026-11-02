using ProjetoIntegradorBack.Domain.DTOs;
using Prime.Architecture.Mediator.Standard.Requests;

namespace ProjetoIntegradorBack.Api.Commands;

public record UpdateServiceCommand(Guid Id, Guid EstablishmentId, string Name, string? Description, int DurationMinutes, decimal Price) : IRequest<ServiceDto>;
