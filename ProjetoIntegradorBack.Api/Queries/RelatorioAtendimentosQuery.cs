using ProjetoIntegradorBack.Domain.DTOs; using Prime.Architecture.Mediator.Standard.Requests;
namespace ProjetoIntegradorBack.Api.Queries; public record RelatorioAtendimentosQuery(Guid EstabelecimentoId, DateTime Inicio, DateTime Fim) : IRequest<RelatorioAtendimentosDto>;
