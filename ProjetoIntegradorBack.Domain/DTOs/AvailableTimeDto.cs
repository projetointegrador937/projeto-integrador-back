using System.Text.Json.Serialization;

namespace ProjetoIntegradorBack.Domain.DTOs
{
    public class AvailableTimeDto
    {
        [JsonPropertyName("horario")]
        public TimeOnly Time { get; set; }

        [JsonPropertyName("disponivel")]
        public bool Available { get; set; } = true;
    }
}
