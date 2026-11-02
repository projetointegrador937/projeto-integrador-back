using ProjetoIntegradorBack.Domain.Enums;

namespace ProjetoIntegradorBack.Domain.Entities
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public Guid EstablishmentId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid ServiceId { get; set; }
        public DateTime DateTime { get; set; }
        public StatusAppointment Status { get; set; } = StatusAppointment.Pendente;
        public string? Notes { get; set; }
        public decimal TotalAmount { get; set; }
        public string? BookingNumber { get; set; }
        public bool LembreteEnviado { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ClientUser Cliente { get; set; } = null!;
        public Establishment Establishment { get; set; } = null!;
        public Employee Employee { get; set; } = null!;
        public Service Service { get; set; } = null!;
    }
}