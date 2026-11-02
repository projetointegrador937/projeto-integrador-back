using ProjetoIntegradorBack.Domain.Enums;
using ProjetoIntegradorBack.Shared.Extensions;
using ProjetoIntegradorBack.Shared.Pattern;

namespace ProjetoIntegradorBack.Domain.Entities
{
    public class ClientUser : User
    {
        public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
        public AddressUser? Address { get; set; }
        private static readonly UserType[] AllowedUserTypes =
        {
            UserType.Cliente,
            UserType.Barbeiro,
            UserType.Barbearia,
            UserType.Admin
        };
        public static Result<ClientUser> CreateUser(
            string email,
            string password,
            string profilePictureUrl,
            string name)
        {
            return Create(email, password, profilePictureUrl, name, UserType.Cliente);
        }


        public static Result<ClientUser> CreateBarber(
            string email,
            string password,
            string profilePictureUrl,
            string name)
        {
            return Create(email, password, profilePictureUrl, name, UserType.Barbeiro);
        }

        public static Result<ClientUser> CreateBarberShopOwner(
           string email,
           string password,
           string profilePictureUrl,
           string name)
        {
            return Create(email, password, profilePictureUrl, name, UserType.Barbearia);
        }

        public static Result<ClientUser> CreateAdmin(
            string email,
            string password,
            string profilePictureUrl,
            string name)
        {
            return Create(email, password, profilePictureUrl, name, UserType.Admin);
        }

        public static Result<ClientUser> Create(
            string email,
            string password,
            string profilePictureUrl,
            string name,
            UserType userType)
        {
            var failure = email.EnsureEmail() ??
                          name.EnsureNotEmpty("Name");

            if (failure != null)
                return Result<ClientUser>.AsFailure(failure);

            if (!User.IsValidType(userType) || !AllowedUserTypes.Contains(userType))
                return Result<ClientUser>.AsFailure(
                    new Failure(400, "Invalid UserType"));

            var newEmail = ProjetoIntegradorBack.Domain.ValueObjects.Email.CreateEmail(email);

            var user = new ClientUser
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = newEmail.Success,
                Password = password,
                UserType = userType,
                ProfilePictureUrl = profilePictureUrl
            };

            return Result<ClientUser>.AsSuccess(user);
        }
    }
}
