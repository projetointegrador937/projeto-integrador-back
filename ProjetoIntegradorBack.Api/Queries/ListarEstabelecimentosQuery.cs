using ProjetoIntegradorBack.Domain.DTOs;
using Prime.Architecture.Mediator.Standard.Requests;
namespace ProjetoIntegradorBack.Api.Queries;
public record ListarEstabelecimentosQuery(string? Busca, string? Cidade, int Pagina, int Tamanho) : IRequest<PagedResultDto<EstabelecimentoDto>>;
