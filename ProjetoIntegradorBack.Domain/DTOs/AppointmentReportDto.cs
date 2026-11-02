using ProjetoIntegradorBack.Domain.Models;

namespace ProjetoIntegradorBack.Domain.DTOs
{
    public class AppointmentReportDto
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public ResumoAtendimentosInfo Resumo { get; set; } = new();
    }
}