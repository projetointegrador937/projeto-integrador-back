using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Domain.Enums;
using ProjetoIntegradorBack.Shared.Pattern;

namespace ProjetoIntegradorBack.Domain.Interfaces
{
    public interface IContatoRepository
    {
        Task<Result<Contato>> AdicionarAsync(Contato contato, CancellationToken ct = default);
        Task<Result<IReadOnlyList<Contato>>> ListarAsync(StatusContato? status, int pagina, int tamanho, CancellationToken ct = default);
        Task<Result<int>> ContarAsync(StatusContato? status, CancellationToken ct = default);
        Task<Result<Contato>> AtualizarStatusAsync(Guid id, StatusContato status, CancellationToken ct = default);
    }
}