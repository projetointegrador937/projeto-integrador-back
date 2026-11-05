using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Shared.Pattern;

namespace ProjetoIntegradorBack.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Result<Employee>> GetPorIdAsync(Guid id, CancellationToken ct = default);
                Task<Result<Employee>> GetPorUserIdAsync(Guid userId, CancellationToken ct = default);
        Task<Result<IReadOnlyList<Employee>>> ListPorEstablishmentAsync(Guid establishmentId, CancellationToken ct = default);
        Task<Result<Employee>> AddAsync(Employee funcionario, CancellationToken ct = default);
        Task<Result<Employee>> UpdateAsync(Employee funcionario, CancellationToken ct = default);
        Task<Result<string>> AlterarVinculoAsync(Guid id, bool ativo, CancellationToken ct = default);
    }
}