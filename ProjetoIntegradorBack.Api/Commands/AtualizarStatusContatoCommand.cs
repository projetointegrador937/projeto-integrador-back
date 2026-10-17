using ProjetoIntegradorBack.Domain.DTOs;
using ProjetoIntegradorBack.Domain.Enums;
using Prime.Architecture.Mediator.Standard.Requests;
namespace ProjetoIntegradorBack.Api.Commands;
public record AtualizarStatusContatoCommand(Guid Id, StatusContato Status) : IRequest<ContatoDto>;
