using ProjetoIntegradorBack.Domain.DTOs;
using Prime.Architecture.Mediator.Standard.Requests;
namespace ProjetoIntegradorBack.Api.Commands;
public record RemarcarAgendamentoCommand(Guid Id, DateTime NovaDataHora) : IRequest<AgendamentoDto>;
