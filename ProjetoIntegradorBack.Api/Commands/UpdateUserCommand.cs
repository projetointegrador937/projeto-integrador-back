using Prime.Architecture.Mediator.Standard.Requests;
using ProjetoIntegradorBack.Domain.DTOs;

namespace ProjetoIntegradorBack.Api.Commands;

public sealed record UpdateUserCommand(
    string Name,
    string Email,
    string? Phone,
    string? ProfilePictureUrl,
    AddressDto Address) : IRequest<UserDto>;
