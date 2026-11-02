using ProjetoIntegradorBack.Api.Commands; using ProjetoIntegradorBack.Api.Queries; using Microsoft.AspNetCore.Mvc; using Microsoft.AspNetCore.Authorization; using Prime.Architecture.Mediator.Standard;
namespace ProjetoIntegradorBack.Api.Controllers;
[ApiController, Route("api/v1/establishments")]
public class EstablishmentController(Mediator medaytor) : ControllerBase
{
 [HttpGet] public async Task<IActionResult> List(string? busca,string? cidade,int page=1,int size=20,CancellationToken ct=default)=>Ok(await medaytor.Send(new ListEstablishmentsQuery(busca,cidade,page,size),ct));
 [HttpGet("{id:guid}")] public async Task<IActionResult> Get(Guid id,CancellationToken ct=default)=>Ok(await medaytor.Send(new GetEstablishmentQuery(id),ct));
 [Authorize(Policy = "Barbearia")][HttpPost] public async Task<IActionResult> Criar(CreateEstablishmentCommand c,CancellationToken ct=default)=>Ok(await medaytor.Send(c,ct));
 [Authorize(Policy = "Barbearia")][HttpPut("{id:guid}")] public async Task<IActionResult> Update(Guid id,UpdateEstablishmentCommand c,CancellationToken ct=default)=>Ok(await medaytor.Send(c with {Id=id},ct));
 [Authorize(Policy = "Barbearia")][HttpPost("employees")] public async Task<IActionResult> CreateEmployee(CreateEmployeeCommand command,CancellationToken ct=default)=>Ok(await medaytor.Send(command,ct));
 [HttpGet("{id:guid}/employees")] public async Task<IActionResult> Employees(Guid id,CancellationToken ct=default)=>Ok(await medaytor.Send(new ListEmployeesQuery(id),ct));
 [HttpGet("{id:guid}/services")] public async Task<IActionResult> Services(Guid id,CancellationToken ct=default)=>Ok(await medaytor.Send(new ListServicesQuery(id),ct));

}
