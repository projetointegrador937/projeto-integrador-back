using ProjetoIntegradorBack.Domain.Enums;
using ProjetoIntegradorBack.Domain.ValueObjects;

namespace ProjetoIntegradorBack.Domain.Entities
{
    public abstract class User
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public Email Email { get; set; }
        public string Password { get; init; }
        public string Phone { get; set; }
        public UserType UserType { get; init; }
        public string ProfilePictureUrl { get; set; } = string.Empty;

        public bool IsCliente => UserType == UserType.Cliente;
        public bool IsBarbeiro => UserType == UserType.Barbeiro;
        public bool IsBarbearia => UserType == UserType.Barbearia;
        public bool IsAdmin => UserType == UserType.Admin;

        public bool HasRole(UserType role) => UserType == role;

        public static bool IsValidType(UserType userType) =>
            userType is UserType.Cliente or UserType.Barbeiro or UserType.Barbearia or UserType.Admin;
    }
}
