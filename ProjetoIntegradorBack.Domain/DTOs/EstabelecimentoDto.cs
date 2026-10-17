namespace ProjetoIntegradorBack.Domain.DTOs
{
    public class EstabelecimentoDto
    {
        public Guid Id { get; set; }
        public string RazaoSocial { get; set; } = string.Empty;
        public string NomeFantasia { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string HorarioFuncionamento { get; set; } = string.Empty;
        public bool Ativo { get; set; }
        public EnderecoDto Endereco { get; set; } = new();
        public DateTime CriadoEm { get; set; }
    }
}