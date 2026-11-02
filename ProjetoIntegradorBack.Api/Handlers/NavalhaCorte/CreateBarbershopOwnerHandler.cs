using BCryptHash = BCrypt.Net.BCrypt;
using Prime.Architecture.Mediator.Standard.Requests;
using ProjetoIntegradorBack.Api.Commands;
using ProjetoIntegradorBack.Domain.DTOs;
using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Domain.Interfaces;

namespace ProjetoIntegradorBack.Api.Handlers.NavalhaCorte;

public sealed class CreateBarbershopOwnerHandler(IUserRepository repository)
    : IRequestHandler<CreateBarbershopOwnerCommand, UserSummaryDto>
{
    public async Task<UserSummaryDto> Handle(CreateBarbershopOwnerCommand request, CancellationToken ct)
    {
        var resultado = ClientUser.CreateBarberShopOwner(
            request.Email,
            BCryptHash.HashPassword(request.Password),
            request.ProfilePictureUrl,
            request.Name
        );

        if (resultado.IsFailure)
            throw new InvalidOperationException(resultado.Failure!.ToString());

        var user = resultado.Success!;
        await repository.AddUserAsync(user);

        return new UserSummaryDto
        {
            Id = user.Id,
            Name = user.Name,
            Contact = user.Phone
        };
    }
}
