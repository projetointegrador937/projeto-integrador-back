using ProjetoIntegradorBack.Domain.DTOs;
using Prime.Architecture.Mediator.Standard.Requests;

namespace ProjetoIntegradorBack.Api.Commands;

public record CriarAgendamentoNovoCommand(Guid ClienteId, Guid EstabelecimentoId, Guid FuncionarioId, Guid ServicoId, DateTime DataHora, string? Observacoes) : IRequest<AgendamentoDto>;
