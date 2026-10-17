using ProjetoIntegradorBack.Domain.Enums;

namespace ProjetoIntegradorBack.Domain.DTOs
{
    public class UsuarioDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Telefone { get; set; }
        public UserType Tipo { get; set; }
        public string ProfilePictureUrl { get; set; } = string.Empty;
    }
}