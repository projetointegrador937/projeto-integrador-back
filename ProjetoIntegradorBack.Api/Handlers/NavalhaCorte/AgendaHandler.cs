using ProjetoIntegradorBack.Api.Queries;
using ProjetoIntegradorBack.Domain.DTOs;
using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Domain.Interfaces;
using Prime.Architecture.Mapper.Interfaces;
using Prime.Architecture.Mediator.Standard.Requests;

namespace ProjetoIntegradorBack.Api.Handlers.NavalhaCorte;

public sealed class AgendaMensalHandler(IAgendamentoRepository repository, IMapper mapper) : IRequestHandler<AgendaMensalQuery, List<AgendamentoDiaDto>>
{
    public async Task<List<AgendamentoDiaDto>> Handle(AgendaMensalQuery r, CancellationToken ct)
    { var x=await repository.ObterPorFuncionarioNoMesAsync(r.FuncionarioId,r.Ano,r.Mes,ct); if(x.IsFailure)return []; return x.Success!.GroupBy(a=>DateOnly.FromDateTime(a.DataHora)).Select(g=>new AgendamentoDiaDto{Dia=g.Key,TotalAgendamentos=g.Count(),Agendamentos=g.Select(a=>mapper.Map<Agendamento,AgendamentoDto>(a)).ToList()}).OrderBy(x=>x.Dia).ToList(); }
}

public sealed class AgendaDiaHandler(IAgendamentoRepository repository, IMapper mapper) : IRequestHandler<AgendaDiaQuery, List<AgendamentoDto>>
{
    public async Task<List<AgendamentoDto>> Handle(AgendaDiaQuery r, CancellationToken ct) { var x=await repository.ObterPorFuncionarioNoDiaAsync(r.FuncionarioId,r.Dia,ct); return x.IsSuccess?x.Success!.Select(a=>mapper.Map<Agendamento,AgendamentoDto>(a)).ToList():[]; }
}
