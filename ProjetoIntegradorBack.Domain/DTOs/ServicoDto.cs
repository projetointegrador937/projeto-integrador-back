namespace ProjetoIntegradorBack.Domain.DTOs
{
    public class ServicoDto
    {
        public Guid Id { get; set; }
        public Guid EstabelecimentoId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public int DuracaoMinutos { get; set; }
        public decimal Preco { get; set; }
        public bool Ativo { get; set; }
    }
}