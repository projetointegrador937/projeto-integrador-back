using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProjetoIntegradorBack.Api.Json;

public sealed class BusinessHoursJsonConverter : JsonConverter<string>
{
    public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType switch
        {
            JsonTokenType.String => reader.GetString() ?? string.Empty,
            JsonTokenType.StartObject or JsonTokenType.StartArray =>
                JsonDocument.ParseValue(ref reader).RootElement.GetRawText(),
            JsonTokenType.Null => string.Empty,
            _ => throw new JsonException("businessHours deve ser uma string, objeto ou array JSON.")
        };
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            writer.WriteStringValue(value);
            return;
        }

        try
        {
            using var document = JsonDocument.Parse(value);
            document.RootElement.WriteTo(writer);
        }
        catch (JsonException)
        {
            writer.WriteStringValue(value);
        }
    }
}
