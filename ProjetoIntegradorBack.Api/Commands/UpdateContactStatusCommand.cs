using ProjetoIntegradorBack.Domain.DTOs;
using ProjetoIntegradorBack.Domain.Enums;
using Prime.Architecture.Mediator.Standard.Requests;
namespace ProjetoIntegradorBack.Api.Commands;
public record UpdateContactStatusCommand(Guid Id, StatusContact Status) : IRequest<ContactDto>;
