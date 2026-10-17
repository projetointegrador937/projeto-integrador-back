using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Domain.Interfaces;
using ProjetoIntegradorBack.Infrastructure.Context;
using ProjetoIntegradorBack.Shared.Pattern;
using Microsoft.EntityFrameworkCore;

namespace ProjetoIntegradorBack.Infrastructure.Repositories
{
    public class BarberShopRepository : IBarberShopRepository
    {
        private readonly ProjetoIntegradorBackDbContext _context;
        public BarberShopRepository(ProjetoIntegradorBackDbContext context)
        {
            _context = context;
        }

        public async Task<Result<string>> AddBarberAsync(ClientUser user, Barber barber)
        {

            try
            {
                await _context.Users.AddAsync(user);
                await _context.Barbers.AddAsync(barber);
                await _context.SaveChangesAsync();
                return Result<string>.AsSuccess("Barber and User added successfully.");
            }
            catch (Exception ex)
            {
                return Result<string>.AsFailure(new Failure(ex.HResult, $"Error adding Barber: {ex.Message}"));
            }
        }

        public async Task<Result<string>> AddBarberShopAsync(BarberShop barberShop)
        {
            try
            {
                await _context.BarberShops.AddAsync(barberShop);
                await _context.SaveChangesAsync();
                return Result<string>.AsSuccess("BarberShop added successfully.");
            }
            catch (Exception ex)
            {
                return Result<string>.AsFailure(new Failure(ex.HResult, $"Error adding BarberShop: {ex.Message}"));
            }
        }

        public async Task<Result<string>> AddServiceAsync(BarberShopService service)
        {
            try
            {
                await _context.Services.AddAsync(service);
                await _context.SaveChangesAsync();
                return Result<string>.AsSuccess("Service added successfully.");
            }
            catch (Exception ex)
            {
                return Result<string>.AsFailure(new Failure(ex.HResult, $"Error adding Service: {ex.Message}"));
            }
        }

        public async Task<Result<string>> AddBarberScheduleAsync(Barber barber, BarberSchedule barberSchedule)
        {
            try
            {
                await _context.Barbers.AddAsync(barber);
                await _context.BarberSchedules.AddAsync(barberSchedule);
                await _context.SaveChangesAsync();
                return Result<string>.AsSuccess("BarberSchedule created successfully.");
            }
            catch (Exception ex)
            {
                return Result<string>.AsFailure(new Failure(ex.HResult, $"Error creating BarberSchedule: {ex.Message}"));
            }
        }

        public async Task<Result<string>> AddBusinessHoursAsync(BarberShop barberShop, BusinessHours businessHours)
        {
            try
            {
                await _context.BarberShops.AddAsync(barberShop);
                await _context.BusinessHours.AddAsync(businessHours);
                await _context.SaveChangesAsync();
                return Result<string>.AsSuccess("BusinessHours created successfully.");
            }
            catch (Exception ex)
            {
                return Result<string>.AsFailure(new Failure(ex.HResult, $"Error creating BusinessHours: {ex.Message}"));
            }
        }

        public async Task<Result<string>> DeleteServiceAsync(Guid id)
        {
            try
            {
                var service = await _context.Services.FindAsync(id);
                if (service == null)
                {
                    return Result<string>.AsFailure(new Failure(404, "Service not found."));
                }
                _context.Services.Remove(service);
                await _context.SaveChangesAsync();
                return Result<string>.AsSuccess("Service deleted successfully.");

            }
            catch (Exception ex)
            {
                return Result<string>.AsFailure(new Failure(ex.HResult, $"Error deleting Service: {ex.Message}"));
            }
        }

        public async Task<Result<IEnumerable<BarberShop>>> GetAllBarberShopsAsync()
        {
            try
            {
                var barberShops = await _context.BarberShops.ToListAsync();
                return Result<IEnumerable<BarberShop>>.AsSuccess(barberShops);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<BarberShop>>.AsFailure(new Failure(ex.HResult, $"Error retrieving BarberShops: {ex.Message}"));
            }
        }

        public async Task<Result<IEnumerable<BarberShopService>>> GetAllServicesAsync()
        {
            try
            {
                var services = await _context.Services.ToListAsync();
                return Result<IEnumerable<BarberShopService>>.AsSuccess(services);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<BarberShopService>>.AsFailure(new Failure(ex.HResult, $"Error retrieving Services: {ex.Message}"));
            }
        }

        public async Task<Result<BarberShop>> GetBarberShopByIdAsync(Guid id)
        {
            try
            {
                var barberShop = await _context.BarberShops.FindAsync(id);
                if (barberShop == null)
                {
                    return Result<BarberShop>.AsFailure(new Failure(404, "BarberShop not found."));
                }
                return Result<BarberShop>.AsSuccess(barberShop);
            }
            catch (Exception ex)
            {
                return Result<BarberShop>.AsFailure(new Failure(ex.HResult, $"Error retrieving BarberShop: {ex.Message}"));
            }
        }

        public async Task<Result<BarberShopService>> GetServiceByIdAsync(Guid barberShopId, Guid id)
        {
            try
            {
                var service = await _context.Services.Where
                    (s => s.Id == id).FirstOrDefaultAsync();

                if (service == null)
                {
                    return Result<BarberShopService>.AsFailure(new Failure(404, "Service not found."));
                }
                return Result<BarberShopService>.AsSuccess(service);
            }
            catch (Exception ex)
            {
                return Result<BarberShopService>.AsFailure(new Failure(ex.HResult, $"Error retrieving Service: {ex.Message}"));
            }
        }

        public async Task<Result<string>> UpdateBarberShopAsync(BarberShop barberShop)
        {
            try
            {
                _context.BarberShops.Update(barberShop);
                await _context.SaveChangesAsync();
                return Result<string>.AsSuccess("BarberShop updated successfully.");
            }
            catch (Exception ex)
            {
                return Result<string>.AsFailure(new Failure(ex.HResult, $"Error updating BarberShop: {ex.Message}"));
            }
        }

        public async Task<Result<string>> UpdateServiceAsync(BarberShopService service)
        {
            try
            {
                _context.Services.Update(service);
                await _context.SaveChangesAsync();
                return Result<string>.AsSuccess("Service updated successfully.");
            }
            catch (Exception ex)
            {
                return Result<string>.AsFailure(new Failure(ex.HResult, $"Error updating Service: {ex.Message}"));
            }
        }

        public async Task<Result<Barber>> GetBarberByIdAsync(Guid id)
        {
            try
            {
                var barber = await _context.Barbers.FindAsync(id);
                if (barber == null)
                {
                    return Result<Barber>.AsFailure(new Failure(404, "Barber not found."));
                }
                return Result<Barber>.AsSuccess(barber);
            }
            catch (Exception ex)
            {
                return Result<Barber>.AsFailure(new Failure(ex.HResult, $"Error retrieving Barber: {ex.Message}"));
            }
        }

        public async Task<Result<BarberSchedule>> GetBarberSchedulesAsync(Guid barberShopId, Guid barberId)
        {
            try
            {
                var schedule = await _context.BarberSchedules
                    .Where(s => s.BarberShopId == barberShopId && s.BarberId == barberId)
                    .FirstOrDefaultAsync();
                if (schedule == null)
                {
                    return Result<BarberSchedule>.AsFailure(new Failure(404, "Barber schedule not found."));
                }
                return Result<BarberSchedule>.AsSuccess(schedule);
            }
            catch (Exception ex)
            {
                return
                    Result<BarberSchedule>.AsFailure(new
                    Failure(ex.HResult, $"Error retrieving Barber Schedule: {ex.Message}"));
            }
        }

        public async Task<Result<BusinessHours>> GetBusinessHoursAsync(Guid barberShopId)
        {
            try
            {
                var businessHours = await _context.BusinessHours
                    .Where(bh => bh.BarberShopId == barberShopId)
                    .FirstOrDefaultAsync();
                if (businessHours == null)
                {
                    return Result<BusinessHours>.AsFailure(new Failure(404, "Business hours not found."));
                }
                return Result<BusinessHours>.AsSuccess(businessHours);
            }
            catch (Exception ex)
            {
                return Result<BusinessHours>.AsFailure(new Failure(ex.HResult, $"Error retrieving Business Hours: {ex.Message}"));
            }
        }
    }
}
