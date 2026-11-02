using ProjetoIntegradorBack.Domain.DTOs;
using Prime.Architecture.Mediator.Standard.Requests;

namespace ProjetoIntegradorBack.Api.Commands;

public record UpdateEstablishmentCommand(Guid Id, string TradeName, string Phone, string Email, string Description, string BusinessHours, AddressDto Address) : IRequest<EstablishmentDto>;
