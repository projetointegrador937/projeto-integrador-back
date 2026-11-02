using ProjetoIntegradorBack.Domain.Enums;

namespace ProjetoIntegradorBack.Domain.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public UserType Type { get; set; }
        public string ProfilePictureUrl { get; set; } = string.Empty;
    }
}