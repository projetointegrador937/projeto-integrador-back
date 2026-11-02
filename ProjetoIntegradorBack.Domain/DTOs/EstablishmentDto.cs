namespace ProjetoIntegradorBack.Domain.DTOs
{
    public class EstablishmentDto
    {
        public Guid Id { get; set; }
        public string LegalName { get; set; } = string.Empty;
        public string TradeName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string BusinessHours { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public AddressDto Address { get; set; } = new();
        public DateTime CreatedAt { get; set; }
    }
}