namespace ProjetoIntegradorBack.Domain.Models
{
    public class AppointmentModel
    {
        public int DurationInMinutes { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}
