using ProjetoIntegradorBack.Domain.DTOs;
using ProjetoIntegradorBack.Api.Json;
using System.Text.Json.Serialization;
using Prime.Architecture.Mediator.Standard.Requests;

namespace ProjetoIntegradorBack.Api.Commands;

public record UpdateEstablishmentCommand(Guid Id, string TradeName, string Phone, string Email, string Description, [property: JsonConverter(typeof(BusinessHoursJsonConverter))] string BusinessHours, AddressDto Address) : IRequest<EstablishmentDto>;
