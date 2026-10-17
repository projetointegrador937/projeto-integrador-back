using Prime.Architecture.Mediator.Standard.Requests;
namespace ProjetoIntegradorBack.Api.Commands;
public record CancelarAgendamentoCommand(Guid Id) : IRequest<string>;
