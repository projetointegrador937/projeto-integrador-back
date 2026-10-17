using ProjetoIntegradorBack.Domain.DTOs; using Prime.Architecture.Mediator.Standard.Requests;
namespace ProjetoIntegradorBack.Api.Queries; public record AgendaDiaQuery(Guid FuncionarioId, DateOnly Dia) : IRequest<List<AgendamentoDto>>;
