using ProjetoIntegradorBack.Domain.DTOs;
using Prime.Architecture.Mediator.Standard.Requests;

namespace ProjetoIntegradorBack.Api.Commands;

public record CreateAppointmentCommand(Guid ClientId, Guid EstablishmentId, Guid EmployeeId, Guid ServiceId, DateTime DateTime, string? Notes) : IRequest<AppointmentDto>;
