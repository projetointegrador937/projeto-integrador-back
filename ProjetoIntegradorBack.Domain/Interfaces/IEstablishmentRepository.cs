using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Shared.Pattern;

namespace ProjetoIntegradorBack.Domain.Interfaces
{
    public interface IEstablishmentRepository
    {
        Task<Result<Establishment>> GetPorIdAsync(Guid id, CancellationToken ct = default);
        Task<Result<IReadOnlyList<Establishment>>> ListAsync(string? busca, string? cidade, int page, int size, CancellationToken ct = default);
        Task<Result<Establishment>> AddAsync(Establishment estabelecimento, CancellationToken ct = default);
        Task<Result<Establishment>> UpdateAsync(Establishment estabelecimento, CancellationToken ct = default);
        Task<Result<string>> DesativarAsync(Guid id, CancellationToken ct = default);
        Task<Result<int>> ContarAsync(string? busca, string? cidade, CancellationToken ct = default);
    }
}