using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Domain.Interfaces;
using ProjetoIntegradorBack.Domain.Models;
using ProjetoIntegradorBack.Infrastructure.Context;
using ProjetoIntegradorBack.Shared.Pattern;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace ProjetoIntegradorBack.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ProjetoIntegradorBackDbContext _context;
        private readonly string _connectionString;
        public AppointmentRepository(ProjetoIntegradorBackDbContext context)
        {
            _context = context;
            _connectionString = context.Database.GetConnectionString();
        }
        public async Task<Result<string>> AddAppointmentAsync(Appointment appointment)
        {
            try
            {
                await _context.Appointments.AddAsync(appointment);
                await _context.SaveChangesAsync();
                return Result<string>.AsSuccess("Appointment added successfully.");

            }
            catch (Exception ex)
            {
                return Result<string>.AsFailure(new Failure(500, $"An error occurred while adding the appointment: {ex.Message}"));
            }
        }

        public Task<Result<string>> DeleteAppointmentAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Appointment>> GetAppointmentByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<IEnumerable<AppointmentModel>>> GetAppointmentsByBarberIdAsync(Guid barberShopId, Guid barberId, DateOnly date)
        {
            try
            {
                var conn = new NpgsqlConnection(_connectionString);
                const string query = @"select ap.""CustomerName"", 
                                 S.""DurationInMinutes"", 
                                 ap.""StartDateTime"", 
                                 ap.""EndDateTime"" 
                            from public.""Appointments"" ap
                            inner join public.""Services"" S on ap.""ServiceId"" = S.""Id""
                            inner join public.""Barbers"" b on ap.""BarberId"" = b.""Id""
                            where b.""Id"" = @barberId                            
                            and b.""BarberShopId"" = @barberShopId
                            and ap.""StartDateTime""::date = @date";
                var dateTime = date.ToDateTime(TimeOnly.MinValue);
                var appointments = await conn.QueryAsync<AppointmentModel>(query, new { barberId, barberShopId, date = dateTime });
                if(appointments == null)
                {
                    return Result<IEnumerable<AppointmentModel>>.AsFailure(new Failure(404, "No appointments found"));
                }

                return Result<IEnumerable<AppointmentModel>>.AsSuccess(appointments);

            }
            catch (Exception ex)
            {
                return (Result<IEnumerable<AppointmentModel>>.AsFailure(new Failure(400, ex.Message)));
            }
        }

        public async Task<Result<IEnumerable<Appointment>>> GetAppointmentsByCustomerNameAsync(string customerName)
        {
            try
            {
                var appointments = await _context.Appointments.Where(a => a.CustomerName == customerName).ToListAsync();
                return Result<IEnumerable<Appointment>>.AsSuccess(appointments);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<Appointment>>.AsFailure(new Failure(400, ex.Message));
            }
        }

        public async Task<Result<Appointment>> GetLastAppoinmentAsync(Guid barberId)
        {
            try
            {
                var lastAppointment = await _context.Appointments
                    .Where(a => a.BarberId == barberId)
                    .OrderByDescending(a => a.StartDateTime)
                    .FirstOrDefaultAsync();

                return Result<Appointment>.AsSuccess(lastAppointment ?? new Appointment { });
            }
            catch (Exception ex)
            {
                return Result<Appointment>.AsFailure(new Failure(400, ex.Message));
            }
        }

        public Task<Result<string>> UpdateAppointmentAsync(Appointment appointment)
        {
            throw new NotImplementedException();
        }
    }
}
