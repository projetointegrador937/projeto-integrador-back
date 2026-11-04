using ProjetoIntegradorBack.Api.Commands;
using ProjetoIntegradorBack.Api.Queries;
using ProjetoIntegradorBack.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Prime.Architecture.Mediator.Standard;
using System.Security.Claims;

namespace ProjetoIntegradorBack.Api.Controllers;

[ApiController, Authorize, Route("api/v1/appointments")]
public class AppointmentController(Mediator medaytor) : ControllerBase
{
    private Guid CurrentUserId =>
        Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : Guid.Empty;

    [Authorize(Policy = "Cliente")]
    [HttpGet("mine")]
    public async Task<IActionResult> Meus(StatusAppointment? status, int page = 1, CancellationToken ct = default)
        => Ok(await medaytor.Send(new MeusAppointmentsQuery(CurrentUserId, status, page, 20), ct));

    [Authorize(Policy = "BarbeiroOuBarbearia")]
    [HttpGet("schedule")]
    public async Task<IActionResult> Agenda(Guid employeeId, int month, int year, CancellationToken ct = default)
        => Ok(await medaytor.Send(new AgendaMensalQuery(employeeId, month, year), ct));

    [Authorize(Policy = "BarbeiroOuBarbearia")]
    [HttpGet("schedule/{day}")]
    public async Task<IActionResult> Date(DateOnly day, Guid employeeId, CancellationToken ct = default)
        => Ok(await medaytor.Send(new AgendaDateQuery(employeeId, day), ct));

    [Authorize(Policy = "Cliente")]
    [HttpPost]
    public async Task<IActionResult> Criar(CreateAppointmentCommand c, CancellationToken ct = default)
        => Ok(await medaytor.Send(c with { ClientId = CurrentUserId }, ct));

    [Authorize(Policy = "Cliente")]
    [HttpPut("{id:guid}/reschedule")]
    public async Task<IActionResult> Remarcar(Guid id, [FromBody] RescheduleAppointmentCommand command, CancellationToken ct = default)
        => Ok(await medaytor.Send(command with { Id = id }, ct));

    [Authorize(Policy = "Cliente")]
    [HttpPatch("{id:guid}/cancel")]
    public async Task<IActionResult> Cancelar(Guid id, CancellationToken ct = default)
        => Ok(await medaytor.Send(new CancelAppointmentCommand(id), ct));
}
