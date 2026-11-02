using ProjetoIntegradorBack.Domain.DTOs; using Prime.Architecture.Mediator.Standard.Requests;
namespace ProjetoIntegradorBack.Api.Queries; public record RelatorioAtendimentosQuery(Guid EstablishmentId, DateTime Start, DateTime End) : IRequest<AppointmentReportDto>;
