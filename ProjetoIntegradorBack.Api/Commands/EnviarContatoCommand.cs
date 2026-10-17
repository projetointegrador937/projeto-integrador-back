using ProjetoIntegradorBack.Domain.DTOs;
using Prime.Architecture.Mediator.Standard.Requests;
namespace ProjetoIntegradorBack.Api.Commands;
public record EnviarContatoCommand(string Assunto, string Nome, string Sobrenome, string Email, string? NumeroReserva, string Descricao) : IRequest<ContatoDto>;
