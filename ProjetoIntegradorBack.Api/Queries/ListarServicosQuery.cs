using ProjetoIntegradorBack.Domain.DTOs;
using Prime.Architecture.Mediator.Standard.Requests;
namespace ProjetoIntegradorBack.Api.Queries;
public record ListarServicosQuery(Guid EstabelecimentoId) : IRequest<List<ServicoDto>>;
