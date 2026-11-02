using ProjetoIntegradorBack.Domain.Models;
using ProjetoIntegradorBack.Shared.Pattern;

namespace ProjetoIntegradorBack.Domain.Interfaces
{
    public interface IRelatorioRepository
    {
        Task<Result<IReadOnlyList<ComissaoEmployeeInfo>>> GetCommissionsPorMonthAsync(Guid establishmentId, int month, int year, Guid? employeeId, CancellationToken ct = default);
        Task<Result<ResumoAtendimentosInfo>> GetResumoAtendimentosAsync(Guid establishmentId, DateTime start, DateTime end, CancellationToken ct = default);
    }
}