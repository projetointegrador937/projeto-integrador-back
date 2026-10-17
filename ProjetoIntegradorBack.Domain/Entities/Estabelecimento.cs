using ProjetoIntegradorBack.Domain.ValueObjects;

namespace ProjetoIntegradorBack.Domain.Entities
{
    public class Estabelecimento
    {
        public Guid Id { get; set; }
        public string RazaoSocial { get; set; } = string.Empty;
        public string NomeFantasia { get; set; } = string.Empty;
        public string Cnpj { get; set; } = string.Empty;
        public string? InscricaoEstadual { get; set; }
        public string Telefone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string HorarioFuncionamento { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
        public DateTime CriadoEm { get; set; }
        public DateTime? AtualizadoEm { get; set; }
        public Endereco Endereco { get; set; } = new();
        public ICollection<Funcionario> Funcionarios { get; set; } = [];
        public ICollection<Servico> Servicos { get; set; } = [];
        public ICollection<Agendamento> Agendamentos { get; set; } = [];
    }
}