using System.ComponentModel.DataAnnotations;

namespace ProjetoIntegradorBack.Api.Commands;

public sealed class RefreshTokenCommand
{
    [Required(ErrorMessage = "Refresh token é obrigatório.")]
    public string RefreshToken { get; init; } = string.Empty;
}
