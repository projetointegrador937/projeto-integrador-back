using ProjetoIntegradorBack.Domain.DTOs; using Prime.Architecture.Mediator.Standard.Requests;
namespace ProjetoIntegradorBack.Api.Queries; public record RelatorioComissaoQuery(Guid EstablishmentId, int Month, int Year, Guid? EmployeeId) : IRequest<CommissionReportDto>;
