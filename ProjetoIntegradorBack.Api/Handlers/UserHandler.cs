using BCryptHash = BCrypt.Net.BCrypt;
using FluentValidation;
using ProjetoIntegradorBack.Api.Commands;
using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Domain.Interfaces;
using ProjetoIntegradorBack.Shared.Pattern;

namespace ProjetoIntegradorBack.Api.Handlers;

public class UserHandler(
    IUserRepository userRepository,
    IValidator<CreateUserCommand> validator)
{
    public async Task<Result<string>> HandleCreateUser(CreateUserCommand command)
    {
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            return Result<string>.AsFailure(new Failure(
                400,
                string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage))));
        }

        var userResult = ClientUser.CreateUser(
            command.Email.Trim(),
            BCryptHash.HashPassword(command.Password),
            command.ProfilePictureUrl ?? string.Empty,
            command.Name.Trim());

        if (userResult.IsFailure || userResult.Success is null)
            return Result<string>.AsFailure(userResult.Failure!);

        var existingUser = await userRepository.GetUserByEmailAsync(command.Email);
        if (existingUser.IsSuccess)
            return Result<string>.AsFailure(new Failure(409, "E-mail já cadastrado."));

        var savedUser = await userRepository.AddUserAsync(userResult.Success);
        if (savedUser.IsFailure)
            return Result<string>.AsFailure(savedUser.Failure!);

        return Result<string>.AsSuccess("User created successfully");
    }
}
