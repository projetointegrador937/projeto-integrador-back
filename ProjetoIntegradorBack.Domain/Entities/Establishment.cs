using ProjetoIntegradorBack.Domain.ValueObjects;

namespace ProjetoIntegradorBack.Domain.Entities
{
    public class Establishment
    {
        public Guid Id { get; set; }
        public string LegalName { get; set; } = string.Empty;
        public string TradeName { get; set; } = string.Empty;
        public string Cnpj { get; set; } = string.Empty;
        public string? StateRegistration { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string BusinessHours { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Address Address { get; set; } = new();
        public ICollection<Employee> Employees { get; set; } = [];
        public ICollection<Service> Services { get; set; } = [];
        public ICollection<Appointment> Appointments { get; set; } = [];
    }
}