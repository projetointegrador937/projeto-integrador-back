using ProjetoIntegradorBack.Domain.Models;

namespace ProjetoIntegradorBack.Domain.DTOs
{
    public class RelatorioAtendimentosDto
    {
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public ResumoAtendimentosInfo Resumo { get; set; } = new();
    }
}