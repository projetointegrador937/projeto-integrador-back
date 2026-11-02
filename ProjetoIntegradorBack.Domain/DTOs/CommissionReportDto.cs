using ProjetoIntegradorBack.Domain.Models;

namespace ProjetoIntegradorBack.Domain.DTOs
{
    public class CommissionReportDto
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public List<ComissaoEmployeeInfo> Commissions { get; set; } = [];
        public decimal TotalRevenue { get; set; }
        public decimal TotalCommissions { get; set; }
    }
}