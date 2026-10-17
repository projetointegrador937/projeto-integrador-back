namespace ProjetoIntegradorBack.Domain.Entities
{
    public class ScheduleBlock
    {
        public Guid Id { get; init; }

        public Guid ProfessionalId { get; init; }

        public DateTime StartAt { get; init; }

        public DateTime EndAt { get; init; }

        public string Reason { get; init; }
    }
}
