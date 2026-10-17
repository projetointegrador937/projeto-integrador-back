using ProjetoIntegradorBack.Shared.Pattern;

namespace ProjetoIntegradorBack.Domain.Interfaces
{
    public interface IDisponibilidadeService
    {
        Task<Result<IReadOnlyList<TimeOnly>>> ObterHorariosDisponiveisAsync(Guid estabelecimentoId, Guid funcionarioId, Guid servicoId, DateOnly data, CancellationToken ct = default);
    }
}