using ProjetoIntegradorBack.Domain.DTOs;
using Prime.Architecture.Mediator.Standard.Requests;
namespace ProjetoIntegradorBack.Api.Queries;
public record ListEmployeesQuery(Guid EstablishmentId) : IRequest<List<EmployeeDto>>;
