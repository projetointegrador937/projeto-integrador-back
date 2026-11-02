using ProjetoIntegradorBack.Domain.DTOs;
using Prime.Architecture.Mediator.Standard.Requests;

namespace ProjetoIntegradorBack.Api.Commands;

public record UpdateEmployeeCommand(Guid Id, Guid EstablishmentId, string Name, string Email, string Phone, string Role, decimal CommissionPercentage) : IRequest<EmployeeDto>;
