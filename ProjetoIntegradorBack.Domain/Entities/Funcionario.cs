namespace ProjetoIntegradorBack.Domain.Entities
{
    public class Funcionario
    {
        public Guid Id { get; set; }
        public Guid EstabelecimentoId { get; set; }
        public Guid UsuarioId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Funcao { get; set; } = string.Empty;
        public decimal PercentualComissao { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime CriadoEm { get; set; }
        public Estabelecimento Estabelecimento { get; set; } = null!;
        public ClientUser Usuario { get; set; } = null!;
        public ICollection<Agendamento> Agendamentos { get; set; } = [];
    }
}