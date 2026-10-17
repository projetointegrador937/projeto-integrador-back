namespace ProjetoIntegradorBack.Domain.Models
{
    public class ResumoAtendimentosInfo
    {
        public int TotalAgendamentos { get; set; }
        public int Confirmados { get; set; }
        public int Concluidos { get; set; }
        public int Cancelados { get; set; }
        public decimal FaturamentoTotal { get; set; }
    }
}