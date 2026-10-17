namespace ProjetoIntegradorBack.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; init; }
        public Guid UserId { get; init; }
        public DateTime BirthDate { get; init; }
    }
}
