using ProjetoIntegradorBack.Domain.DTOs;
using ProjetoIntegradorBack.Domain.ValueObjects;
using Prime.Architecture.Mediator.Standard.Requests;

namespace ProjetoIntegradorBack.Api.Commands;

public record CreateEstablishmentCommand(string LegalName, string TradeName, string Cnpj, string? StateRegistration, string Phone, string Email, string Description, string BusinessHours, AddressDto Address) : IRequest<EstablishmentDto>;
