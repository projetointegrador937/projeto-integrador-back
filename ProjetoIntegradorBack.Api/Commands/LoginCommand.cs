using System.ComponentModel.DataAnnotations;

namespace ProjetoIntegradorBack.Api.Commands
{
    public class LoginCommand
    {
        [Required]
        public string Email { get; init; }
        public string Password { get; init; } = string.Empty;
        public string GoogleKey { get; init; } = string.Empty;   
        public string FacebookKey { get; init; } = string.Empty;
        public string AppleKey { get; init; } = string.Empty;        
    }
}
