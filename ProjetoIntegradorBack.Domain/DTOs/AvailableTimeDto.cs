namespace ProjetoIntegradorBack.Domain.DTOs
{
    public class AvailableTimeDto
    {
        public TimeOnly Time { get; set; }
        public bool Available { get; set; } = true;
    }
}