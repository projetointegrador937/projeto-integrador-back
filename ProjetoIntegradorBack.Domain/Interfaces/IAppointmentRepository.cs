using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Domain.Models;
using ProjetoIntegradorBack.Shared.Pattern;

namespace ProjetoIntegradorBack.Domain.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<Result<Appointment>> GetAppointmentByIdAsync(Guid id);
        Task<Result<IEnumerable<AppointmentModel>>> GetAppointmentsByBarberIdAsync(Guid barberShopId,Guid barberId,DateOnly date);
        Task<Result<Appointment>> GetLastAppoinmentAsync(Guid barberId);
        Task<Result<IEnumerable<Appointment>>> GetAppointmentsByCustomerNameAsync(string customerName);
        Task<Result<string>> AddAppointmentAsync(Appointment appointment);
        Task<Result<string>> UpdateAppointmentAsync(Appointment appointment);
        Task<Result<string>> DeleteAppointmentAsync(Guid id);
    }
}
