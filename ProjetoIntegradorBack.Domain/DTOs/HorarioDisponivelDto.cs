namespace ProjetoIntegradorBack.Domain.DTOs
{
    public class HorarioDisponivelDto
    {
        public TimeOnly Horario { get; set; }
        public bool Disponivel { get; set; } = true;
    }
}