using System.Text.Json.Serialization;
using ProjetoIntegradorBack.Domain.Enums;

namespace ProjetoIntegradorBack.Domain.DTOs
{
    public class AppointmentDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("clienteId")]
        public Guid ClientId { get; set; }

        [JsonPropertyName("nomeCliente")]
        public string NameClient { get; set; } = string.Empty;

        [JsonPropertyName("estabelecimentoId")]
        public Guid EstablishmentId { get; set; }

        [JsonPropertyName("nomeEstabelecimento")]
        public string NameEstablishment { get; set; } = string.Empty;

        [JsonPropertyName("enderecoEstabelecimento")]
        public AddressDto? EstablishmentAddress { get; set; }

        [JsonPropertyName("telefoneEstabelecimento")]
        public string EstablishmentPhone { get; set; } = string.Empty;

        [JsonPropertyName("emailEstabelecimento")]
        public string EstablishmentEmail { get; set; } = string.Empty;

        [JsonPropertyName("funcionarioId")]
        public Guid EmployeeId { get; set; }

        [JsonPropertyName("nomeFuncionario")]
        public string NameEmployee { get; set; } = string.Empty;

        [JsonPropertyName("servicoId")]
        public Guid ServiceId { get; set; }

        [JsonPropertyName("nomeServico")]
        public string NameService { get; set; } = string.Empty;

        [JsonPropertyName("dataHora")]
        public DateTime DateTime { get; set; }

        [JsonPropertyName("status")]
        public StatusAppointment Status { get; set; }

        [JsonPropertyName("observacoes")]
        public string? Notes { get; set; }

        [JsonPropertyName("valorTotal")]
        public decimal TotalAmount { get; set; }

        [JsonPropertyName("numeroReserva")]
        public string? BookingNumber { get; set; }

        [JsonPropertyName("criadoEm")]
        public DateTime CreatedAt { get; set; }
    }
}
