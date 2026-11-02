using Prime.Architecture.Mediator.Standard.Requests;
using ProjetoIntegradorBack.Api.Commands;
using ProjetoIntegradorBack.Shared.Pattern;

namespace ProjetoIntegradorBack.Api.Handlers;

public sealed class CreateUserCommandHandler(UserHandler handler) : IRequestHandler<CreateUserCommand, Result<string>>
{
    public Task<Result<string>> Handle(CreateUserCommand request, CancellationToken ct) => handler.HandleCreateUser(request);
}



