using ProjetoIntegradorBack.Domain.ValueObjects;
using ProjetoIntegradorBack.Shared.Extensions;
using ProjetoIntegradorBack.Shared.Pattern;

namespace ProjetoIntegradorBack.Domain.Entities
{
    public class BarberShop
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string Phone { get; init; }
        public Email Email { get; init; }
        public string Logo { get; init; } = string.Empty;

        public static Result<BarberShop> CreateBarberShop(
           string name,
           string email,
           string description,
           string phone,
           string logo)
        {
            var failure = email.EnsureEmail() ??
                name.EnsureNotEmpty("Name");
            var newEmail = ProjetoIntegradorBack.Domain.ValueObjects.Email.CreateEmail(email);
            if (failure != null)
                return Result<BarberShop>.AsFailure(failure);
            var user = new BarberShop
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = newEmail.Success,
                Description = description,
                Phone = phone,
                Logo = logo
            };
            return Result<BarberShop>.AsSuccess(user);
        }
    }

}           
