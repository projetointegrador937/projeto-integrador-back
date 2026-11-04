using ProjetoIntegradorBack.Domain.DTOs;
using Prime.Architecture.Mediator.Standard.Requests;

namespace ProjetoIntegradorBack.Api.Queries;

public record GetAvailabilityQuery(
    Guid EstablishmentId,
    DateOnly Date,
    Guid? EmployeeId,
    Guid? ServiceId
) : IRequest<List<AvailableTimeDto>>;
