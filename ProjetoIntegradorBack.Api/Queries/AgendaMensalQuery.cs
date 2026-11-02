using ProjetoIntegradorBack.Domain.DTOs; using Prime.Architecture.Mediator.Standard.Requests;
namespace ProjetoIntegradorBack.Api.Queries; public record AgendaMensalQuery(Guid EmployeeId, int Month, int Year) : IRequest<List<AppointmentDayDto>>;
