namespace ProjetoIntegradorBack.Domain.DTOs
{
    public class AgendamentoDiaDto
    {
        public DateOnly Dia { get; set; }
        public int TotalAgendamentos { get; set; }
        public List<AgendamentoDto> Agendamentos { get; set; } = [];
    }
}