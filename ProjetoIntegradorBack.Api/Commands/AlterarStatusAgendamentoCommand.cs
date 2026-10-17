using ProjetoIntegradorBack.Domain.Enums;
using Prime.Architecture.Mediator.Standard.Requests;
namespace ProjetoIntegradorBack.Api.Commands;
public record AlterarStatusAgendamentoCommand(Guid Id, StatusAgendamento Status) : IRequest<string>;
