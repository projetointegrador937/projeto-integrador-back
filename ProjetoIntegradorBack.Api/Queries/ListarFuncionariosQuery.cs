using ProjetoIntegradorBack.Domain.DTOs;
using Prime.Architecture.Mediator.Standard.Requests;
namespace ProjetoIntegradorBack.Api.Queries;
public record ListarFuncionariosQuery(Guid EstabelecimentoId) : IRequest<List<FuncionarioDto>>;
