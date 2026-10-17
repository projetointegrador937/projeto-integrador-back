using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Shared.Pattern;

namespace ProjetoIntegradorBack.Domain.Interfaces
{
    public interface IEstabelecimentoRepository
    {
        Task<Result<Estabelecimento>> ObterPorIdAsync(Guid id, CancellationToken ct = default);
        Task<Result<IReadOnlyList<Estabelecimento>>> ListarAsync(string? busca, string? cidade, int pagina, int tamanho, CancellationToken ct = default);
        Task<Result<Estabelecimento>> AdicionarAsync(Estabelecimento estabelecimento, CancellationToken ct = default);
        Task<Result<Estabelecimento>> AtualizarAsync(Estabelecimento estabelecimento, CancellationToken ct = default);
        Task<Result<string>> DesativarAsync(Guid id, CancellationToken ct = default);
        Task<Result<int>> ContarAsync(string? busca, string? cidade, CancellationToken ct = default);
    }
}