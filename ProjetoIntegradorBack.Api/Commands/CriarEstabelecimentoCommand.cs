using ProjetoIntegradorBack.Domain.DTOs;
using ProjetoIntegradorBack.Domain.ValueObjects;
using Prime.Architecture.Mediator.Standard.Requests;

namespace ProjetoIntegradorBack.Api.Commands;

public record CriarEstabelecimentoCommand(string RazaoSocial, string NomeFantasia, string Cnpj, string? InscricaoEstadual, string Telefone, string Email, string Descricao, string HorarioFuncionamento, EnderecoDto Endereco) : IRequest<EstabelecimentoDto>;
