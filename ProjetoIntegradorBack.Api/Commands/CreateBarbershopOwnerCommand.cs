using Prime.Architecture.Mediator.Standard.Requests;
using ProjetoIntegradorBack.Domain.DTOs;
using System.ComponentModel.DataAnnotations;

namespace ProjetoIntegradorBack.Api.Commands;

public sealed class CreateBarbershopOwnerCommand : IRequest<UserSummaryDto>
{
    [Required]
    [StringLength(255, ErrorMessage = "E-mail deve ter entre 6 e 255 caracteres.", MinimumLength = 6)]
    public string Email { get; init; } = string.Empty;

    [Required]
    [StringLength(35, ErrorMessage = "Password deve ter entre 8 e 35 caracteres.", MinimumLength = 8)]
    public string Password { get; init; } = string.Empty;

    [Required]
    [StringLength(255, ErrorMessage = "Name deve ter entre 6 e 255 caracteres.", MinimumLength = 6)]
    public string Name { get; init; } = string.Empty;

    public string ProfilePictureUrl { get; init; } = string.Empty;
}
