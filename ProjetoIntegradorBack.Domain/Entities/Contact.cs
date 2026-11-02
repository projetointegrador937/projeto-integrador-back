using ProjetoIntegradorBack.Domain.Enums;

namespace ProjetoIntegradorBack.Domain.Entities
{
    public class Contact
    {
        public Guid Id { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? BookingNumber { get; set; }
        public string Description { get; set; } = string.Empty;
        public StatusContact Status { get; set; } = StatusContact.Aberto;
        public DateTime CreatedAt { get; set; }
    }
}