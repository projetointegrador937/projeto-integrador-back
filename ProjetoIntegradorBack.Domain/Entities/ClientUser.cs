using ProjetoIntegradorBack.Domain.Enums;
using ProjetoIntegradorBack.Shared.Extensions;
using ProjetoIntegradorBack.Shared.Pattern;

namespace ProjetoIntegradorBack.Domain.Entities
{
    public class ClientUser : User
    {
        private static readonly UserType[] AllowedUserTypes =
        {
            UserType.User,
            UserType.Barber,
            UserType.BarberShopOwner
        };
        public static Result<ClientUser> CreateUser(
            string email,
            string password,
            string profilePictureUrl,
            string name)
        {
            return Create(email, password, profilePictureUrl, name, UserType.User);
        }


        public static Result<ClientUser> CreateBarber(
            string email,
            string password,
            string profilePictureUrl,
            string name)
        {
            return Create(email, password, profilePictureUrl, name, UserType.Barber);
        }

        public static Result<ClientUser> CreateBarberShopOwner(
           string email,
           string password,
           string profilePictureUrl,
           string name)
        {
            return Create(email, password, profilePictureUrl, name, UserType.BarberShopOwner);
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

            if (!AllowedUserTypes.Contains(userType))
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
