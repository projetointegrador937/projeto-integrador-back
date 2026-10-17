using ProjetoIntegradorBack.Domain.DTOs; using Prime.Architecture.Mediator.Standard.Requests;
namespace ProjetoIntegradorBack.Api.Queries; public record RelatorioComissaoQuery(Guid EstabelecimentoId, int Mes, int Ano, Guid? FuncionarioId) : IRequest<RelatorioComissaoDto>;
