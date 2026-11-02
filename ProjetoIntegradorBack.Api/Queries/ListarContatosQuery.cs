using ProjetoIntegradorBack.Domain.DTOs; using ProjetoIntegradorBack.Domain.Enums; using Prime.Architecture.Mediator.Standard.Requests;
namespace ProjetoIntegradorBack.Api.Queries; public record ListContactsQuery(StatusContact? Status, int Page, int Tamanho) : IRequest<PagedResultDto<ContactDto>>;
