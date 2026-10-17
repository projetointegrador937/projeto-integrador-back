using ProjetoIntegradorBack.Api.Commands;
using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Domain.Interfaces;
using ProjetoIntegradorBack.Shared.Pattern;
using FluentValidation;
using System.Data;

namespace ProjetoIntegradorBack.Api.Handlers
{
    public class UserHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<CreateUserCommand> _validator;

        public UserHandler(IUserRepository userRepository, IValidator<CreateUserCommand> validator)
        {
            _userRepository = userRepository;
            _validator = validator;
        }

        public async Task<Result<string>> HandleCreateUser(CreateUserCommand command)
        {
            var validationResult = _validator.Validate(command);
            if (!validationResult.IsValid)
            {
                return Result<string>.AsFailure(new Failure(
                    400, string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)))
                );
            }

            var user = ClientUser.CreateUser(
                command.Email,
                command.Password,
                command.ProfilePictureUrl,
                command.Name
            );

            var userDb = await _userRepository.AddUserAsync(user.Success);
            return Result<string>.AsSuccess($"User created successfully");
        }

        public async Task<Result<string>> HandleCreateBarberShopOwner(CreateUserCommand command)
        {
            var validationResult = _validator.Validate(command);
            if (!validationResult.IsValid)
            {
                return Result<string>.AsFailure(new Failure(
                    400, string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)))
                );
            }

            var user = ClientUser.CreateBarberShopOwner(
                command.Email,
                command.Password,
                command.ProfilePictureUrl,
                command.Name
            );

            var userDb = await _userRepository.AddUserAsync(user.Success);
            return Result<string>.AsSuccess($"User created successfully");
        }
    }
}
