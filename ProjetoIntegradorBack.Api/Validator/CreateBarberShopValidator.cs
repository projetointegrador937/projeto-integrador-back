using ProjetoIntegradorBack.Api.Commands;
using FluentValidation;

namespace ProjetoIntegradorBack.Api.Validator
{
    public class CreateBarberShopValidator : AbstractValidator<CreateBarberShopCommand>
    {
        public CreateBarberShopValidator() 
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}
