using ProjetoIntegradorBack.Api.Commands; using ProjetoIntegradorBack.Api.Queries; using ProjetoIntegradorBack.Domain.Enums; using Microsoft.AspNetCore.Mvc; using Prime.Architecture.Mediator.Standard;
namespace ProjetoIntegradorBack.Api.Controllers;
[ApiController, Route("api/v1/contato")]
public class ContatoController(Mediator mediator) : ControllerBase
{
 [HttpPost] public async Task<IActionResult> Enviar(EnviarContatoCommand c,CancellationToken ct=default)=>Ok(await mediator.Send(c,ct));
 [HttpGet] public async Task<IActionResult> Listar(StatusContato? status,int pagina=1,CancellationToken ct=default)=>Ok(await mediator.Send(new ListarContatosQuery(status,pagina,20),ct));
 [HttpPatch("{id:guid}/status")] public async Task<IActionResult> Status(Guid id,AtualizarStatusContatoCommand c,CancellationToken ct=default)=>Ok(await mediator.Send(c with {Id=id},ct));
}
