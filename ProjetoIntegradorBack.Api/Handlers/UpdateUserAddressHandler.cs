using Microsoft.AspNetCore.Http;
using Prime.Architecture.Mediator.Standard.Requests;
using Prime.Architecture.Mapper.Interfaces;
using ProjetoIntegradorBack.Api.Commands;
using ProjetoIntegradorBack.Domain.DTOs;
using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ProjetoIntegradorBack.Api.Handlers;

public sealed class UpdateUserAddressHandler(
    ProjetoIntegradorBackDbContext context,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<UpdateUserCommand, UserDto>
{
    public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var userIdValue = httpContextAccessor.HttpContext?.User.FindFirst("sub")?.Value
            ?? httpContextAccessor.HttpContext?.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(userIdValue, out var userId))
            throw new UnauthorizedAccessException("Usuário não autenticado.");

        var user = await context.Users.SingleOrDefaultAsync(x => x.Id == userId, cancellationToken);
        if (user is null)
            throw new KeyNotFoundException("Usuário não encontrado.");

        var email = ProjetoIntegradorBack.Domain.ValueObjects.Email.CreateEmail(request.Email);
        if (email.IsFailure)
            throw new ArgumentException("E-mail inválido.");

        user.Name = request.Name;
        user.Email = email.Success!;
        user.Phone = request.Phone!;
        user.ProfilePictureUrl = request.ProfilePictureUrl ?? string.Empty;

        var address = await context.AddresssUsers
            .SingleOrDefaultAsync(x => x.UserId == userId, cancellationToken);

        if (address is null)
        {
            address = new AddressUser { Id = Guid.NewGuid(), UserId = userId };
            context.AddresssUsers.Add(address);
        }

        address.Street = request.Address.Street;
        address.Number = request.Address.Number;
        address.AdditionalInfo = request.Address.AdditionalInfo;
        address.Neighborhood = request.Address.Neighborhood;
        address.PostalCode = request.Address.PostalCode;
        address.City = request.Address.City;
        address.State = request.Address.State;
        address.Latitude = request.Address.Latitude;
        address.Longitude = request.Address.Longitude;

        await context.SaveChangesAsync(cancellationToken);
        return mapper.Map<ClientUser, UserDto>(user);
    }
}
