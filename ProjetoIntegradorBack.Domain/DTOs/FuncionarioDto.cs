namespace ProjetoIntegradorBack.Domain.DTOs
{
    public class FuncionarioDto
    {
        public Guid Id { get; set; }
        public Guid EstabelecimentoId { get; set; }
        public Guid UsuarioId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Funcao { get; set; } = string.Empty;
        public decimal PercentualComissao { get; set; }
        public bool Ativo { get; set; }
        public DateTime CriadoEm { get; set; }
    }
}