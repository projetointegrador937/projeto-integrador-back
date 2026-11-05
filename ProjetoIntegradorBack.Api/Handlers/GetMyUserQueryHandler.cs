using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Prime.Architecture.Mediator.Standard.Requests;
using ProjetoIntegradorBack.Api.Queries;
using ProjetoIntegradorBack.Domain.DTOs;
using ProjetoIntegradorBack.Domain.Interfaces;

namespace ProjetoIntegradorBack.Api.Handlers.NavalhaCorte;

public sealed class GetMyUserQueryHandler(
    IUserRepository repository,
    IEmployeeRepository employeeRepository,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<GetMyUserQuery, UserSummaryDto>
{
    public async Task<UserSummaryDto> Handle(GetMyUserQuery request, CancellationToken ct)
    {
        var userIdValue = httpContextAccessor.HttpContext?.User.FindFirst("sub")?.Value
            ?? httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(userIdValue, out var userId))
            throw new UnauthorizedAccessException("Usuário não autenticado.");

        var result = await repository.GetUserByIdAsync(userId);
        if (result.IsFailure)
            throw new KeyNotFoundException("Usuário não encontrado.");

        var user = result.Success!;

        var employee = await employeeRepository.GetPorUserIdAsync(user.Id, ct);

        return new UserSummaryDto
        {
            Id = user.Id,
            EmployeeId = employee.IsSuccess ? employee.Success!.Id : null,
            Name = user.Name,
            Contact = user.Phone,
            Address = user.Address is null ? null : new AddressDto
            {
                Street = user.Address.Street,
                Number = user.Address.Number,
                AdditionalInfo = user.Address.AdditionalInfo,
                Neighborhood = user.Address.Neighborhood,
                PostalCode = user.Address.PostalCode,
                City = user.Address.City,
                State = user.Address.State,
                Latitude = user.Address.Latitude,
                Longitude = user.Address.Longitude
            }
        };
    }
}
