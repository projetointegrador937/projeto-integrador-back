using ProjetoIntegradorBack.Domain.Enums;
using ProjetoIntegradorBack.Domain.ValueObjects;

namespace ProjetoIntegradorBack.Domain.Entities
{
    public abstract class User
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public Email Email { get; init; }
        public string Password { get; init; }
        public string Phone { get; init; }
        public UserType UserType { get; init; }
        public string ProfilePictureUrl { get; init; } = string.Empty;
    }
}
