using ProjetoIntegradorBack.Api.Commands;
using ProjetoIntegradorBack.Api.Queries;
using ProjetoIntegradorBack.Domain.DTOs;
using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Domain.Enums;
using ProjetoIntegradorBack.Domain.Interfaces;
using Prime.Architecture.Mapper.Interfaces;
using Prime.Architecture.Mediator.Standard.Requests;

namespace ProjetoIntegradorBack.Api.Handlers.NavalhaCorte;

public sealed class MeusAgendamentosHandler(IAgendamentoRepository repository, IMapper mapper) : IRequestHandler<MeusAgendamentosQuery, PagedResultDto<AgendamentoDto>>
{
    public async Task<PagedResultDto<AgendamentoDto>> Handle(MeusAgendamentosQuery request, CancellationToken ct)
    {
        var list = await repository.ObterPorClienteAsync(request.ClienteId, request.Status, request.Pagina, request.Tamanho, ct);
        var total = await repository.ContarPorClienteAsync(request.ClienteId, request.Status, ct);
        return new() { Dados = list.IsSuccess ? list.Success!.Select(x => mapper.Map<Agendamento, AgendamentoDto>(x)).ToList() : [], Pagina = request.Pagina, TamanhoPagina = request.Tamanho, Total = total.IsSuccess ? total.Success : 0 };
    }
}

public sealed class CriarAgendamentoNovoHandler(IAgendamentoRepository repository, IServicoRepository servicos, IMapper mapper) : IRequestHandler<CriarAgendamentoNovoCommand, AgendamentoDto>
{
    public async Task<AgendamentoDto> Handle(CriarAgendamentoNovoCommand request, CancellationToken ct)
    {
        var service = await servicos.ObterPorIdAsync(request.ServicoId, ct);
        if (service.IsFailure) throw new KeyNotFoundException("Serviço não encontrado.");
        var conflict = await repository.ExisteConflitoDeHorarioAsync(request.FuncionarioId, request.DataHora, service.Success!.DuracaoMinutos, ct);
        if (conflict.IsSuccess && conflict.Success) throw new InvalidOperationException("Conflito de horário.");
        var result = await repository.AdicionarAsync(new Agendamento { ClienteId = request.ClienteId, EstabelecimentoId = request.EstabelecimentoId, FuncionarioId = request.FuncionarioId, ServicoId = request.ServicoId, DataHora = request.DataHora, Observacoes = request.Observacoes, ValorTotal = service.Success.Preco, Status = StatusAgendamento.Pendente }, ct);
        if (result.IsFailure) throw new InvalidOperationException(result.Failure!.ToString());
        return mapper.Map<Agendamento, AgendamentoDto>(result.Success!);
    }
}

public sealed class RemarcarAgendamentoHandler(IAgendamentoRepository repository, IMapper mapper) : IRequestHandler<RemarcarAgendamentoCommand, AgendamentoDto>
{
    public async Task<AgendamentoDto> Handle(RemarcarAgendamentoCommand request, CancellationToken ct)
    {
        var found = await repository.ObterPorIdAsync(request.Id, ct); if (found.IsFailure) throw new KeyNotFoundException("Agendamento não encontrado.");
        found.Success!.DataHora = request.NovaDataHora; found.Success.Status = StatusAgendamento.Pendente;
        var result = await repository.AtualizarAsync(found.Success, ct); if (result.IsFailure) throw new InvalidOperationException(result.Failure!.ToString());
        return mapper.Map<Agendamento, AgendamentoDto>(result.Success!);
    }
}

public sealed class CancelarAgendamentoHandler(IAgendamentoRepository repository) : IRequestHandler<CancelarAgendamentoCommand, string>
{
    public async Task<string> Handle(CancelarAgendamentoCommand request, CancellationToken ct)
    { var found = await repository.ObterPorIdAsync(request.Id, ct); if (found.IsFailure) throw new KeyNotFoundException("Agendamento não encontrado."); found.Success!.Status = StatusAgendamento.Cancelado; var result = await repository.AtualizarAsync(found.Success, ct); if (result.IsFailure) throw new InvalidOperationException(result.Failure!.ToString()); return "Agendamento cancelado com sucesso."; }
}

public sealed class AlterarStatusAgendamentoHandler(IAgendamentoRepository repository) : IRequestHandler<AlterarStatusAgendamentoCommand, string>
{
    public async Task<string> Handle(AlterarStatusAgendamentoCommand request, CancellationToken ct)
    { var found = await repository.ObterPorIdAsync(request.Id, ct); if (found.IsFailure) throw new KeyNotFoundException("Agendamento não encontrado."); found.Success!.Status = request.Status; var result = await repository.AtualizarAsync(found.Success, ct); if (result.IsFailure) throw new InvalidOperationException(result.Failure!.ToString()); return "Status atualizado com sucesso."; }
}
