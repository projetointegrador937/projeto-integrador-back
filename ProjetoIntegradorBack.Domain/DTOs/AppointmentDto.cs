using ProjetoIntegradorBack.Domain.Enums;

namespace ProjetoIntegradorBack.Domain.DTOs
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public Guid EstablishmentId { get; set; }
        public string NameEstablishment { get; set; } = string.Empty;
        public Guid EmployeeId { get; set; }
        public string NameEmployee { get; set; } = string.Empty;
        public Guid ServiceId { get; set; }
        public string NameService { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }
        public StatusAppointment Status { get; set; }
        public string? Notes { get; set; }
        public decimal TotalAmount { get; set; }
        public string? BookingNumber { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}