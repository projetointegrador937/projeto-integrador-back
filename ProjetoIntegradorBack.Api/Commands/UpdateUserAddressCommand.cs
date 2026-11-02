using Prime.Architecture.Mediator.Standard.Requests;
using ProjetoIntegradorBack.Domain.DTOs;

namespace ProjetoIntegradorBack.Api.Commands;

public sealed record UpdateUserAddressCommand(AddressDto Address) : IRequest<AddressDto>;
