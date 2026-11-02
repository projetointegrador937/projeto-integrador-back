using ProjetoIntegradorBack.Domain.DTOs;
using Prime.Architecture.Mediator.Standard.Requests;
namespace ProjetoIntegradorBack.Api.Queries;
public record ListServicesQuery(Guid EstablishmentId) : IRequest<List<ServiceDto>>;
