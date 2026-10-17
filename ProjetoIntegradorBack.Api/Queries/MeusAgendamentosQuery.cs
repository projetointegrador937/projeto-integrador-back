using ProjetoIntegradorBack.Domain.DTOs;
using ProjetoIntegradorBack.Domain.Enums;
using Prime.Architecture.Mediator.Standard.Requests;
namespace ProjetoIntegradorBack.Api.Queries;
public record MeusAgendamentosQuery(Guid ClienteId, StatusAgendamento? Status, int Pagina, int Tamanho) : IRequest<PagedResultDto<AgendamentoDto>>;
