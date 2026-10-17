using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Shared.Pattern;

namespace ProjetoIntegradorBack.Domain.Interfaces
{
    public interface IFuncionarioRepository
    {
        Task<Result<Funcionario>> ObterPorIdAsync(Guid id, CancellationToken ct = default);
        Task<Result<IReadOnlyList<Funcionario>>> ListarPorEstabelecimentoAsync(Guid estabelecimentoId, CancellationToken ct = default);
        Task<Result<Funcionario>> AdicionarAsync(Funcionario funcionario, CancellationToken ct = default);
        Task<Result<Funcionario>> AtualizarAsync(Funcionario funcionario, CancellationToken ct = default);
        Task<Result<string>> AlterarVinculoAsync(Guid id, bool ativo, CancellationToken ct = default);
    }
}