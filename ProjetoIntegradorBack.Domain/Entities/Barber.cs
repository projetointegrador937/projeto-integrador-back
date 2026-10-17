using ProjetoIntegradorBack.Shared.Extensions;
using ProjetoIntegradorBack.Shared.Pattern;

namespace ProjetoIntegradorBack.Domain.Entities
{
    public class Barber
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public Guid BarberShopId { get; init; }
        public decimal Commission { get; init; }
        public bool Active { get; init; } = false;

        public static Result<Barber> Create(Guid id,string name, Guid barberShopId, decimal commission = 0)
        {
            var failure = name.EnsureNotEmpty("Name");

            if (failure != null)
                return Result<Barber>.AsFailure(failure);

            return Result<Barber>.AsSuccess(new Barber
            {
                Id = id,
                Name = name,
                BarberShopId = barberShopId,
                Commission = commission
            });           
        }
    }
}

