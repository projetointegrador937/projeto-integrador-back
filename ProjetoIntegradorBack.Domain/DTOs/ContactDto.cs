using ProjetoIntegradorBack.Domain.Enums;

namespace ProjetoIntegradorBack.Domain.DTOs
{
    public class ContactDto
    {
        public Guid Id { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? BookingNumber { get; set; }
        public string Description { get; set; } = string.Empty;
        public StatusContact Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}