using ProjetoIntegradorBack.Domain.DTOs;
using Prime.Architecture.Mediator.Standard.Requests;
namespace ProjetoIntegradorBack.Api.Queries;
public record ListEstablishmentsQuery(string? Busca, string? City, int Page, int Tamanho) : IRequest<PagedResultDto<EstablishmentDto>>;
