namespace ProjetoIntegradorBack.Domain.Entities
{
    public class Subscription
    {
        public Guid Id { get; init; }
        public Guid BarberShopId { get; init; }
        public Guid UserId { get; init; }
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
        public Guid PlanId { get; init; }
    }
}
