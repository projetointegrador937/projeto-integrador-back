using ProjetoIntegradorBack.Domain.Enums;

namespace ProjetoIntegradorBack.Domain.DTOs
{
    public class AgendamentoDto
    {
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public Guid EstabelecimentoId { get; set; }
        public string NomeEstabelecimento { get; set; } = string.Empty;
        public Guid FuncionarioId { get; set; }
        public string NomeFuncionario { get; set; } = string.Empty;
        public Guid ServicoId { get; set; }
        public string NomeServico { get; set; } = string.Empty;
        public DateTime DataHora { get; set; }
        public StatusAgendamento Status { get; set; }
        public string? Observacoes { get; set; }
        public decimal ValorTotal { get; set; }
        public string? NumeroReserva { get; set; }
        public DateTime CriadoEm { get; set; }
    }
}