using ProjetoIntegradorBack.Shared.Pattern;

namespace ProjetoIntegradorBack.Domain.Entities
{
    public class BarberShopService
    {
        public Guid Id { get; set; }
        //public Guid BarberShopId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int DurationInMinutes { get; set; }

        public static Result<BarberShopService> Create(/*Guid barberShopId,*/ string name,decimal price, int durationInMinutes)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result<BarberShopService>.AsFailure(new Failure(400, "Name cannot be empty."));
            }
            if (price <= 0)
            {
                return Result<BarberShopService>.AsFailure(new Failure(400, "Price must be greater than zero."));
            }
            if (durationInMinutes <= 0)
            {
                return Result<BarberShopService>.AsFailure(new Failure(400, "Duration must be greater than zero."));
            }
            var service = new BarberShopService
            {
                Id = Guid.NewGuid(),
                Name = name,                
                Price = price,
                DurationInMinutes = durationInMinutes,
                //BarberShopId = barberShopId
            };
            return Result<BarberShopService>.AsSuccess(service);
        }
    }
}
