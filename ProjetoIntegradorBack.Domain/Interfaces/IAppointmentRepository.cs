using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Domain.Enums;
using ProjetoIntegradorBack.Shared.Pattern;

namespace ProjetoIntegradorBack.Domain.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<Result<Appointment>> GetPorIdAsync(Guid id, CancellationToken ct = default);
        Task<Result<IReadOnlyList<Appointment>>> GetPorClienteAsync(Guid clienteId, StatusAppointment? status, int page, int size, CancellationToken ct = default);
        Task<Result<int>> ContarPorClienteAsync(Guid clienteId, StatusAppointment? status, CancellationToken ct = default);
        Task<Result<IReadOnlyList<Appointment>>> GetPorEmployeeNoMonthAsync(Guid employeeId, int year, int month, CancellationToken ct = default);
        Task<Result<IReadOnlyList<Appointment>>> GetPorEmployeeNoDateAsync(Guid employeeId, DateOnly day, CancellationToken ct = default);
        Task<Result<Appointment>> AddAsync(Appointment agendamento, CancellationToken ct = default);
        Task<Result<Appointment>> UpdateAsync(Appointment agendamento, CancellationToken ct = default);
        Task<Result<bool>> HasTimeConflictAsync(Guid employeeId, DateTime dateHora, int duracaoMin, CancellationToken ct = default);
        Task<Result<IReadOnlyList<Appointment>>> GetPorEstablishmentNoPeriodoAsync(Guid establishmentId, DateTime start, DateTime end, CancellationToken ct = default);
    }
}