using ProjetoIntegradorBack.Domain.DTOs;
using Prime.Architecture.Mediator.Standard.Requests;

namespace ProjetoIntegradorBack.Api.Commands;

public record CriarServicoCommand(Guid EstabelecimentoId, string Nome, string? Descricao, int DuracaoMinutos, decimal Preco) : IRequest<ServicoDto>;
