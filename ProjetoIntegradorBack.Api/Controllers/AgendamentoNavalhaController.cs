using ProjetoIntegradorBack.Api.Commands; using ProjetoIntegradorBack.Api.Queries; using ProjetoIntegradorBack.Domain.Enums; using Microsoft.AspNetCore.Mvc; using Prime.Architecture.Mediator.Standard;
namespace ProjetoIntegradorBack.Api.Controllers;
[ApiController, Route("api/v1/agendamentos")]
public class AgendamentoNavalhaController(Mediator mediator) : ControllerBase
{
 [HttpGet("meus")] public async Task<IActionResult> Meus(Guid clienteId,StatusAgendamento? status,int pagina=1,CancellationToken ct=default)=>Ok(await mediator.Send(new MeusAgendamentosQuery(clienteId,status,pagina,20),ct));
 [HttpGet("agenda")] public async Task<IActionResult> Agenda(Guid funcionarioId,int mes,int ano,CancellationToken ct=default)=>Ok(await mediator.Send(new AgendaMensalQuery(funcionarioId,mes,ano),ct));
 [HttpGet("agenda/{dia}")] public async Task<IActionResult> Dia(DateOnly dia,Guid funcionarioId,CancellationToken ct=default)=>Ok(await mediator.Send(new AgendaDiaQuery(funcionarioId,dia),ct));
 [HttpPost] public async Task<IActionResult> Criar(CriarAgendamentoNovoCommand c,CancellationToken ct=default)=>Ok(await mediator.Send(c,ct));
 [HttpPut("{id:guid}/remarcar")] public async Task<IActionResult> Remarcar(Guid id,DateTime novaDataHora,CancellationToken ct=default)=>Ok(await mediator.Send(new RemarcarAgendamentoCommand(id,novaDataHora),ct));
 [HttpPatch("{id:guid}/cancelar")] public async Task<IActionResult> Cancelar(Guid id,CancellationToken ct=default)=>Ok(await mediator.Send(new CancelarAgendamentoCommand(id),ct));
}
