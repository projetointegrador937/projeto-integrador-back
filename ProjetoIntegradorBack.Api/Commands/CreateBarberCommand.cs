using System.ComponentModel.DataAnnotations;

namespace ProjetoIntegradorBack.Api.Commands
{
    public class CreateBarberCommand
    {
        [Required]
        [MinLength(10, ErrorMessage = "E-mail must contain at least 10 characters")]
        [MaxLength(255, ErrorMessage = "E-mail is too large")]
        public string Email { get; init; }

        [Required]
        [MinLength(8, ErrorMessage = "Password must contain at least 8 characters")]
        [MaxLength(40, ErrorMessage = "Password is too large")]
        public string Password { get; init; }
        public string ProfilePictureUrl { get; init; } = string.Empty;

        [Required]
        [MinLength(8, ErrorMessage = "Name must contain at least 8 characters")]
        [MaxLength(100, ErrorMessage = "Name is too large")]
        public string Name { get; init; }

        [Required]
        [MinLength(11, ErrorMessage = "Phone must contain at least 11 characters")]
        [MaxLength(11, ErrorMessage = "Phone is too large")]
        [Phone(ErrorMessage = "Invalid phone format")]
        public string Phone { get; init; }
        [Required]
        public Guid BarberShopId { get; init; }
        public decimal Commission { get; init; } = 0;

    }    
}
