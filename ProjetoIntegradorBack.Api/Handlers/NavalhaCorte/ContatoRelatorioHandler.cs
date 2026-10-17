using ProjetoIntegradorBack.Api.Commands;
using ProjetoIntegradorBack.Api.Queries;
using ProjetoIntegradorBack.Domain.DTOs;
using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Domain.Interfaces;
using Prime.Architecture.Mapper.Interfaces;
using Prime.Architecture.Mediator.Standard.Requests;

namespace ProjetoIntegradorBack.Api.Handlers.NavalhaCorte;

public sealed class EnviarContatoHandler(IContatoRepository repository, IMapper mapper) : IRequestHandler<EnviarContatoCommand, ContatoDto>
{
    public async Task<ContatoDto> Handle(EnviarContatoCommand r, CancellationToken ct)
    { var x = await repository.AdicionarAsync(new Contato { Assunto=r.Assunto, Nome=r.Nome, Sobrenome=r.Sobrenome, Email=r.Email, NumeroReserva=r.NumeroReserva, Descricao=r.Descricao }, ct); if (x.IsFailure) throw new InvalidOperationException(x.Failure!.ToString()); return mapper.Map<Contato, ContatoDto>(x.Success!); }
}

public sealed class ListarContatosHandler(IContatoRepository repository, IMapper mapper) : IRequestHandler<ListarContatosQuery, PagedResultDto<ContatoDto>>
{
    public async Task<PagedResultDto<ContatoDto>> Handle(ListarContatosQuery r, CancellationToken ct)
    { var x=await repository.ListarAsync(r.Status,r.Pagina,r.Tamanho,ct); var t=await repository.ContarAsync(r.Status,ct); return new(){Dados=x.IsSuccess?x.Success!.Select(c=>mapper.Map<Contato,ContatoDto>(c)).ToList():[],Pagina=r.Pagina,TamanhoPagina=r.Tamanho,Total=t.IsSuccess?t.Success:0}; }
}

public sealed class AtualizarStatusContatoHandler(IContatoRepository repository, IMapper mapper) : IRequestHandler<AtualizarStatusContatoCommand, ContatoDto>
{
    public async Task<ContatoDto> Handle(AtualizarStatusContatoCommand r, CancellationToken ct) { var x=await repository.AtualizarStatusAsync(r.Id,r.Status,ct); if(x.IsFailure) throw new KeyNotFoundException(x.Failure!.ToString()); return mapper.Map<Contato,ContatoDto>(x.Success!); }
}

public sealed class RelatorioComissaoHandler(IRelatorioRepository repository) : IRequestHandler<RelatorioComissaoQuery, RelatorioComissaoDto>
{
    public async Task<RelatorioComissaoDto> Handle(RelatorioComissaoQuery r, CancellationToken ct) { var x=await repository.ObterComissoesPorMesAsync(r.EstabelecimentoId,r.Mes,r.Ano,r.FuncionarioId,ct); if(x.IsFailure) throw new InvalidOperationException(x.Failure!.ToString()); var list=x.Success!.ToList(); return new(){Mes=r.Mes,Ano=r.Ano,Comissoes=list,TotalFaturamento=list.Sum(c=>c.FaturamentoBruto),TotalComissoes=list.Sum(c=>c.ValorComissao)}; }
}

public sealed class RelatorioAtendimentosHandler(IRelatorioRepository repository) : IRequestHandler<RelatorioAtendimentosQuery, RelatorioAtendimentosDto>
{
    public async Task<RelatorioAtendimentosDto> Handle(RelatorioAtendimentosQuery r, CancellationToken ct) { var x=await repository.ObterResumoAtendimentosAsync(r.EstabelecimentoId,r.Inicio,r.Fim,ct); if(x.IsFailure) throw new InvalidOperationException(x.Failure!.ToString()); return new(){Inicio=r.Inicio,Fim=r.Fim,Resumo=x.Success!}; }
}
