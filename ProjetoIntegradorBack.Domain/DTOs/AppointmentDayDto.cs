namespace ProjetoIntegradorBack.Domain.DTOs
{
    public class AppointmentDayDto
    {
        public DateOnly Date { get; set; }
        public int TotalAppointments { get; set; }
        public List<AppointmentDto> Appointments { get; set; } = [];
    }
}