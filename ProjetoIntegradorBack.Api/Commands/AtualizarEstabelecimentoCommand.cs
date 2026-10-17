using ProjetoIntegradorBack.Domain.DTOs;
using Prime.Architecture.Mediator.Standard.Requests;

namespace ProjetoIntegradorBack.Api.Commands;

public record AtualizarEstabelecimentoCommand(Guid Id, string NomeFantasia, string Telefone, string Email, string Descricao, string HorarioFuncionamento, EnderecoDto Endereco) : IRequest<EstabelecimentoDto>;
