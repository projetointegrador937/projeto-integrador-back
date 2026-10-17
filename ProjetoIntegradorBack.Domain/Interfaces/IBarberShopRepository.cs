using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Shared.Pattern;

namespace ProjetoIntegradorBack.Domain.Interfaces
{
    public interface IBarberShopRepository
    {
        Task<Result<BarberShop>> GetBarberShopByIdAsync(Guid id);
        Task<Result<IEnumerable<BarberShop>>> GetAllBarberShopsAsync();
        Task<Result<string>> AddBarberShopAsync(BarberShop barberShop);
        Task<Result<string>> AddBarberAsync(ClientUser user, Barber barber);
        Task<Result<string>> UpdateBarberShopAsync(BarberShop barberShop);
        Task<Result<string>> AddBusinessHoursAsync(BarberShop barberShop, BusinessHours businessHours);
        Task<Result<BusinessHours>> GetBusinessHoursAsync(Guid barberShopId);
        Task<Result<string>> AddBarberScheduleAsync(Barber barber, BarberSchedule barberSchedule);
        Task<Result<BarberShopService>> GetServiceByIdAsync(Guid barberShopId, Guid id);
        Task<Result<Barber>> GetBarberByIdAsync(Guid id);
        Task<Result<IEnumerable<BarberShopService>>> GetAllServicesAsync();
        Task<Result<string>> AddServiceAsync(BarberShopService service);
        Task<Result<string>> UpdateServiceAsync(BarberShopService service);
        Task<Result<string>> DeleteServiceAsync(Guid id);
        Task<Result<BarberSchedule>> GetBarberSchedulesAsync(Guid barberShopId, Guid barberId);
    }
}
