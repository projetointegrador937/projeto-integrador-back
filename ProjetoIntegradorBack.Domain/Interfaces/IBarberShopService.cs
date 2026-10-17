using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Shared.Pattern;

namespace ProjetoIntegradorBack.Domain.Interfaces
{
    public interface IBarberShopService
    {
        Task<Result<IEnumerable<TimeOnly>>> GetBarberAvailableTimesAsync(Guid barberShopId, Guid barberId,Guid serviceId, DateOnly date);
       
    }
}
