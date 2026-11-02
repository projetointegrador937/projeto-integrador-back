using ProjetoIntegradorBack.Domain.DTOs;
using Prime.Architecture.Mediator.Standard.Requests;
namespace ProjetoIntegradorBack.Api.Commands;
public record SendContactCommand(string Subject, string Name, string LastName, string Email, string? BookingNumber, string Description) : IRequest<ContactDto>;
