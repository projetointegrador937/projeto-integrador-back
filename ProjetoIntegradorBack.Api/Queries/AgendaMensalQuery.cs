using ProjetoIntegradorBack.Domain.DTOs; using Prime.Architecture.Mediator.Standard.Requests;
namespace ProjetoIntegradorBack.Api.Queries; public record AgendaMensalQuery(Guid FuncionarioId, int Mes, int Ano) : IRequest<List<AgendamentoDiaDto>>;
