using ProjetoIntegradorBack.Domain.Enums;

namespace ProjetoIntegradorBack.Domain.Entities
{
    public class Agendamento
    {
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public Guid EstabelecimentoId { get; set; }
        public Guid FuncionarioId { get; set; }
        public Guid ServicoId { get; set; }
        public DateTime DataHora { get; set; }
        public StatusAgendamento Status { get; set; } = StatusAgendamento.Pendente;
        public string? Observacoes { get; set; }
        public decimal ValorTotal { get; set; }
        public string? NumeroReserva { get; set; }
        public bool LembreteEnviado { get; set; } = false;
        public DateTime CriadoEm { get; set; }
        public DateTime? AtualizadoEm { get; set; }
        public ClientUser Cliente { get; set; } = null!;
        public Estabelecimento Estabelecimento { get; set; } = null!;
        public Funcionario Funcionario { get; set; } = null!;
        public Servico Servico { get; set; } = null!;
    }
}