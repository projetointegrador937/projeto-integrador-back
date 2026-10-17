namespace ProjetoIntegradorBack.Domain.Entities
{
    public class Plan
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public decimal Price { get; init; }
        public int MaxBarbers { get; init; }
        public int Features { get; init; }
    }
}
