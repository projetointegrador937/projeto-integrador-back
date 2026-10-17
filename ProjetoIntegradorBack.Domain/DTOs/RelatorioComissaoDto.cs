using ProjetoIntegradorBack.Domain.Models;

namespace ProjetoIntegradorBack.Domain.DTOs
{
    public class RelatorioComissaoDto
    {
        public int Mes { get; set; }
        public int Ano { get; set; }
        public List<ComissaoFuncionarioInfo> Comissoes { get; set; } = [];
        public decimal TotalFaturamento { get; set; }
        public decimal TotalComissoes { get; set; }
    }
}