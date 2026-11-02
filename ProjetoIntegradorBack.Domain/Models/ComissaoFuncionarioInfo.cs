namespace ProjetoIntegradorBack.Domain.Models
{
    public class ComissaoEmployeeInfo
    {
        public Guid EmployeeId { get; set; }
        public string NameEmployee { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public decimal CommissionPercentage { get; set; }
        public int TotalAtendimentos { get; set; }
        public decimal FaturamentoBruto { get; set; }
        public decimal ValorComissao { get; set; }
    }
}