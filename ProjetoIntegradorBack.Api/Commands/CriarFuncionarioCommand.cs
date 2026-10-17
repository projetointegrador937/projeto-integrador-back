using ProjetoIntegradorBack.Domain.DTOs;
using Prime.Architecture.Mediator.Standard.Requests;

namespace ProjetoIntegradorBack.Api.Commands;

public record CriarFuncionarioCommand(Guid EstabelecimentoId, Guid UsuarioId, string Nome, string Email, string Telefone, string Funcao, decimal PercentualComissao) : IRequest<FuncionarioDto>;
