using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Domain.Enums;
using ProjetoIntegradorBack.Shared.Pattern;

namespace ProjetoIntegradorBack.Domain.Interfaces
{
    public interface IAgendamentoRepository
    {
        Task<Result<Agendamento>> ObterPorIdAsync(Guid id, CancellationToken ct = default);
        Task<Result<IReadOnlyList<Agendamento>>> ObterPorClienteAsync(Guid clienteId, StatusAgendamento? status, int pagina, int tamanho, CancellationToken ct = default);
        Task<Result<int>> ContarPorClienteAsync(Guid clienteId, StatusAgendamento? status, CancellationToken ct = default);
        Task<Result<IReadOnlyList<Agendamento>>> ObterPorFuncionarioNoMesAsync(Guid funcionarioId, int ano, int mes, CancellationToken ct = default);
        Task<Result<IReadOnlyList<Agendamento>>> ObterPorFuncionarioNoDiaAsync(Guid funcionarioId, DateOnly dia, CancellationToken ct = default);
        Task<Result<Agendamento>> AdicionarAsync(Agendamento agendamento, CancellationToken ct = default);
        Task<Result<Agendamento>> AtualizarAsync(Agendamento agendamento, CancellationToken ct = default);
        Task<Result<bool>> ExisteConflitoDeHorarioAsync(Guid funcionarioId, DateTime dataHora, int duracaoMin, CancellationToken ct = default);
        Task<Result<IReadOnlyList<Agendamento>>> ObterPorEstabelecimentoNoPeriodoAsync(Guid estabelecimentoId, DateTime inicio, DateTime fim, CancellationToken ct = default);
    }
}