using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Shared.Pattern;

namespace ProjetoIntegradorBack.Domain.Interfaces
{
    public interface IServiceRepository
    {
        Task<Result<Service>> GetPorIdAsync(Guid id, CancellationToken ct = default);
        Task<Result<IReadOnlyList<Service>>> ListPorEstablishmentAsync(Guid establishmentId, CancellationToken ct = default);
        Task<Result<Service>> AddAsync(Service servico, CancellationToken ct = default);
        Task<Result<Service>> UpdateAsync(Service servico, CancellationToken ct = default);
        Task<Result<string>> DesativarAsync(Guid id, CancellationToken ct = default);
    }
}