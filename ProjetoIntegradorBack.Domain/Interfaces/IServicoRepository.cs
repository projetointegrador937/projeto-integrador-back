using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Shared.Pattern;

namespace ProjetoIntegradorBack.Domain.Interfaces
{
    public interface IServicoRepository
    {
        Task<Result<Servico>> ObterPorIdAsync(Guid id, CancellationToken ct = default);
        Task<Result<IReadOnlyList<Servico>>> ListarPorEstabelecimentoAsync(Guid estabelecimentoId, CancellationToken ct = default);
        Task<Result<Servico>> AdicionarAsync(Servico servico, CancellationToken ct = default);
        Task<Result<Servico>> AtualizarAsync(Servico servico, CancellationToken ct = default);
        Task<Result<string>> DesativarAsync(Guid id, CancellationToken ct = default);
    }
}