namespace ProjetoIntegradorBack.Domain.Models
{
    public class ComissaoFuncionarioInfo
    {
        public Guid FuncionarioId { get; set; }
        public string NomeFuncionario { get; set; } = string.Empty;
        public string Funcao { get; set; } = string.Empty;
        public decimal PercentualComissao { get; set; }
        public int TotalAtendimentos { get; set; }
        public decimal FaturamentoBruto { get; set; }
        public decimal ValorComissao { get; set; }
    }
}