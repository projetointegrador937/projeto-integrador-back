using ProjetoIntegradorBack.Domain.Models;
using ProjetoIntegradorBack.Shared.Pattern;

namespace ProjetoIntegradorBack.Domain.Interfaces
{
    public interface IRelatorioRepository
    {
        Task<Result<IReadOnlyList<ComissaoFuncionarioInfo>>> ObterComissoesPorMesAsync(Guid estabelecimentoId, int mes, int ano, Guid? funcionarioId, CancellationToken ct = default);
        Task<Result<ResumoAtendimentosInfo>> ObterResumoAtendimentosAsync(Guid estabelecimentoId, DateTime inicio, DateTime fim, CancellationToken ct = default);
    }
}