using ProjetoIntegradorBack.Domain.Enums;

namespace ProjetoIntegradorBack.Domain.Entities
{
    public class Contato
    {
        public Guid Id { get; set; }
        public string Assunto { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Sobrenome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? NumeroReserva { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public StatusContato Status { get; set; } = StatusContato.Aberto;
        public DateTime CriadoEm { get; set; }
    }
}