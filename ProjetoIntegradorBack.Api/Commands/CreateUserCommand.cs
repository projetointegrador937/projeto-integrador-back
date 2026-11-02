using ProjetoIntegradorBack.Domain.Enums;
using Prime.Architecture.Mediator.Standard.Requests;
using ProjetoIntegradorBack.Shared.Pattern;
using System.ComponentModel.DataAnnotations;

namespace ProjetoIntegradorBack.Api.Commands
{
    public class CreateUserCommand : IRequest<Result<string>>
    {
        [Required]
        [StringLength(255, ErrorMessage = "E-mail must have between 8 in 255 characters", MinimumLength = 6)]
        public string Email { get; init; }

        [Required]
        [StringLength(35, ErrorMessage = "Password is large", MinimumLength = 8)]
        public string Password { get; init; }
        public string ProfilePictureUrl { get; init; }      
        [Required]
        [StringLength(255, ErrorMessage = "Name is large", MinimumLength = 6)]
        public string Name { get; init; }
    }
}
