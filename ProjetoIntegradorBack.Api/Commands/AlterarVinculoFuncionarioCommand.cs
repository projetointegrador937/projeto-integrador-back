using Prime.Architecture.Mediator.Standard.Requests;

namespace ProjetoIntegradorBack.Api.Commands;

public record AlterarVinculoFuncionarioCommand(Guid Id, Guid EstabelecimentoId, bool Ativo) : IRequest<string>;
