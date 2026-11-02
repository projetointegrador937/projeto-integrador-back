using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Domain.Enums;
using ProjetoIntegradorBack.Shared.Pattern;

namespace ProjetoIntegradorBack.Domain.Interfaces
{
    public interface IContactRepository
    {
        Task<Result<Contact>> AddAsync(Contact contato, CancellationToken ct = default);
        Task<Result<IReadOnlyList<Contact>>> ListAsync(StatusContact? status, int page, int size, CancellationToken ct = default);
        Task<Result<int>> ContarAsync(StatusContact? status, CancellationToken ct = default);
        Task<Result<Contact>> UpdateStatusAsync(Guid id, StatusContact status, CancellationToken ct = default);
    }
}