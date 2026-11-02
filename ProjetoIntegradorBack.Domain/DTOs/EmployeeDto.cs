namespace ProjetoIntegradorBack.Domain.DTOs
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public Guid EstablishmentId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public decimal CommissionPercentage { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}