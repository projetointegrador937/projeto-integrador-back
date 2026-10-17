using ProjetoIntegradorBack.Api.Commands; using ProjetoIntegradorBack.Api.Queries; using Microsoft.AspNetCore.Mvc; using Prime.Architecture.Mediator.Standard;
namespace ProjetoIntegradorBack.Api.Controllers;
[ApiController, Route("api/v1/estabelecimentos")]
public class EstabelecimentoController(Mediator mediator) : ControllerBase
{
 [HttpGet] public async Task<IActionResult> Listar(string? busca,string? cidade,int pagina=1,int tamanho=20,CancellationToken ct=default)=>Ok(await mediator.Send(new ListarEstabelecimentosQuery(busca,cidade,pagina,tamanho),ct));
 [HttpGet("{id:guid}")] public async Task<IActionResult> Obter(Guid id,CancellationToken ct=default)=>Ok(await mediator.Send(new ObterEstabelecimentoQuery(id),ct));
 [HttpPost] public async Task<IActionResult> Criar(CriarEstabelecimentoCommand c,CancellationToken ct=default)=>Ok(await mediator.Send(c,ct));
 [HttpPut("{id:guid}")] public async Task<IActionResult> Atualizar(Guid id,AtualizarEstabelecimentoCommand c,CancellationToken ct=default)=>Ok(await mediator.Send(c with {Id=id},ct));
 [HttpGet("{id:guid}/funcionarios")] public async Task<IActionResult> Funcionarios(Guid id,CancellationToken ct=default)=>Ok(await mediator.Send(new ListarFuncionariosQuery(id),ct));
 [HttpGet("{id:guid}/servicos")] public async Task<IActionResult> Servicos(Guid id,CancellationToken ct=default)=>Ok(await mediator.Send(new ListarServicosQuery(id),ct));
 [HttpGet("{id:guid}/disponibilidade")] public async Task<IActionResult> Disponibilidade(Guid id,DateOnly data,Guid funcionarioId,Guid servicoId,CancellationToken ct=default)=>Ok(await mediator.Send(new HorariosDisponiveisQuery(id,funcionarioId,servicoId,data),ct));
}
