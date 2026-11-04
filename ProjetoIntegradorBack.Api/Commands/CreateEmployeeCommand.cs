using ProjetoIntegradorBack.Domain.DTOs;
using Prime.Architecture.Mediator.Standard.Requests;

namespace ProjetoIntegradorBack.Api.Commands;

public record CreateEmployeeCommand(
    Guid EstablishmentId,
    string Name,
    string Email,
    string Password,
    string Phone,
    string ProfilePictureUrl,
    string Role) : IRequest<EmployeeDto>;
