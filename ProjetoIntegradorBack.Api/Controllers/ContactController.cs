using ProjetoIntegradorBack.Api.Commands; using ProjetoIntegradorBack.Api.Queries; using ProjetoIntegradorBack.Domain.Enums; using Microsoft.AspNetCore.Mvc; using Prime.Architecture.Mediator.Standard;
namespace ProjetoIntegradorBack.Api.Controllers;
[ApiController, Route("api/v1/contact")]
public class ContactController(Mediator medaytor) : ControllerBase
{
 [HttpPost] public async Task<IActionResult> Enviar(SendContactCommand c,CancellationToken ct=default)=>Ok(await medaytor.Send(c,ct));
 [HttpGet] public async Task<IActionResult> List(StatusContact? status,int page=1,CancellationToken ct=default)=>Ok(await medaytor.Send(new ListContactsQuery(status,page,20),ct));
 [HttpPatch("{id:guid}/status")] public async Task<IActionResult> Status(Guid id,UpdateContactStatusCommand c,CancellationToken ct=default)=>Ok(await medaytor.Send(c with {Id=id},ct));
}
