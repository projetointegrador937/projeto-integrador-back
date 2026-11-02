namespace ProjetoIntegradorBack.Domain.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }
        public Guid EstablishmentId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public decimal CommissionPercentage { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public Establishment Establishment { get; set; } = null!;
        public ClientUser User { get; set; } = null!;
        public ICollection<Appointment> Appointments { get; set; } = [];
    }
}