using ProjetoIntegradorBack.Api.Queries; using Microsoft.AspNetCore.Mvc; using Prime.Architecture.Mediator.Standard;
namespace ProjetoIntegradorBack.Api.Controllers;
[ApiController,Route("api/v1/relatorios")]
public class RelatorioController(Mediator mediator):ControllerBase{[HttpGet("comissoes")]public async Task<IActionResult> Comissoes(Guid estabelecimentoId,int mes,int ano,Guid? funcionarioId,CancellationToken c=default)=>Ok(await mediator.Send(new RelatorioComissaoQuery(estabelecimentoId,mes,ano,funcionarioId),c));[HttpGet("atendimentos")]public async Task<IActionResult> Atendimentos(Guid estabelecimentoId,DateTime inicio,DateTime fim,CancellationToken c=default)=>Ok(await mediator.Send(new RelatorioAtendimentosQuery(estabelecimentoId,inicio,fim),c));}
