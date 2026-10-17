namespace ProjetoIntegradorBack.Api.Commands
{
    public class CreateBarberScheduleCommand
    {
        public Guid BarberShopId { get; set; }
        public Guid BarberId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeOnly Start { get; set; }
        public TimeOnly End { get; set; }
        public TimeOnly? BreakStart { get; set; } = null;
        public TimeOnly? BreakEnd { get; set; } = null;
    }
}