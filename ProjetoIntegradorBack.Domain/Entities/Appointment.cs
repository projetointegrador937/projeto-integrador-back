using ProjetoIntegradorBack.Domain.Enums;

namespace ProjetoIntegradorBack.Domain.Entities
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public Guid BarberId { get; set; }
        public Guid ServiceId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public AppointmentStatus Status { get; set; }

        public static Appointment Create(Guid barberId, Guid serviceId, string customerName, DateTime startDateTime, DateTime endDateTime)
        {
            return new Appointment
            {
                Id = Guid.NewGuid(),
                BarberId = barberId,
                ServiceId = serviceId,
                CustomerName = customerName,
                StartDateTime = startDateTime,
                EndDateTime = endDateTime,
                Status = AppointmentStatus.Confirmed
            };
        }
    }
}