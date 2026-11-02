using Prime.Architecture.Mediator.Standard.Requests;
using ProjetoIntegradorBack.Domain.DTOs;
using System.ComponentModel.DataAnnotations;

namespace ProjetoIntegradorBack.Api.Commands;

public sealed class CreateBarberUserCommand : IRequest<UserSummaryDto>
{
    [Required]
    [StringLength(255, MinimumLength = 6)]
    public string Email { get; init; } = string.Empty;

    [Required]
    [StringLength(35, MinimumLength = 8)]
    public string Password { get; init; } = string.Empty;

    [Required]
    [StringLength(255, MinimumLength = 2)]
    public string Name { get; init; } = string.Empty;

    public string ProfilePictureUrl { get; init; } = string.Empty;
}
