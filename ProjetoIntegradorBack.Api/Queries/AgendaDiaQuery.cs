using ProjetoIntegradorBack.Domain.DTOs; using Prime.Architecture.Mediator.Standard.Requests;
namespace ProjetoIntegradorBack.Api.Queries; public record AgendaDateQuery(Guid EmployeeId, DateOnly Date) : IRequest<List<AppointmentDto>>;
