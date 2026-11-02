using ProjetoIntegradorBack.Domain.DTOs;
using Prime.Architecture.Mediator.Standard.Requests;

namespace ProjetoIntegradorBack.Api.Commands;

public record CreateEmployeeCommand(Guid EstablishmentId, Guid UserId, string Name, string Email, string Phone, string Role, decimal CommissionPercentage) : IRequest<EmployeeDto>;
