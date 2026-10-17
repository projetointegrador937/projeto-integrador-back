using ProjetoIntegradorBack.Api.Commands;
using FluentValidation;

namespace ProjetoIntegradorBack.Api.Validator
{  

    public class CreateBarberValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateBarberValidator()
        {           

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");
          
        }
    }
}
