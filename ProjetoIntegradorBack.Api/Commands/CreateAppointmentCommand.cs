namespace ProjetoIntegradorBack.Api.Commands
{
    public class CreateAppointmentCommand
    {
        public Guid BarberShopId { get; set; }
        public Guid BarberId { get; set; }
        public Guid ServiceId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateOnly Date { get; init; }
        public TimeOnly Time { get; init; }

    }
}
