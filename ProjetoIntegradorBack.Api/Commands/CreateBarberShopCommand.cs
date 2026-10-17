using System.ComponentModel.DataAnnotations;

namespace ProjetoIntegradorBack.Api.Commands
{
    public class CreateBarberShopCommand
    {
        [Required]
        [MinLength(6, ErrorMessage = "Name must contain at least 6 characters")]
        [MaxLength(120, ErrorMessage = "Name is too large")]
        public string Name { get; init; }
        [Required]
        [MinLength(10, ErrorMessage = "Description must contain at least 10 characters")]
        [MaxLength(255, ErrorMessage = "Description is too large")]
        public string Description { get; init; }
        [Required]
        [MinLength(8, ErrorMessage = "Phone must contain at least 8 characters")]
        [MaxLength(11, ErrorMessage = "Phone is too large")]
        [Phone(ErrorMessage = "Invalid phone format")]
        public string Phone { get; init; }
        [MinLength(8, ErrorMessage = "Email must contain at least 8 characters")]
        [MaxLength(255, ErrorMessage = "Email is too large")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; init; } = string.Empty;        
        public string Logo { get; init; } = string.Empty;
        public DayOfWeek DayOfWeek { get; init; }
        public TimeOnly Start { get; init; }
        public TimeOnly End { get; init; }
        public TimeOnly? BreakStart { get; init; }
        public TimeOnly? BreakEnd { get; init; }

    }
}
