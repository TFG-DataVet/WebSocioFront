using System.Text.Json;
using System.Text.Json.Serialization;

namespace SocioWeb.Infrastructure.Converters;

/// <summary>
/// Convierte DateTime desde JSON generado por Java:
///   - String ISO con nanosegundos: "2026-05-12T21:05:46.123456789"
///     (System.Text.Json solo acepta hasta 7 dígitos fraccionarios — se trunca)
///   - Array de Jackson: [2026, 5, 12, 21, 5, 46, 123456789]
/// </summary>
public class FlexibleDateTimeConverter : JsonConverter<DateTime>
{
    public static readonly FlexibleDateTimeConverter Instance = new();

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => ReadCore(ref reader);

    internal static DateTime ReadCore(ref Utf8JsonReader reader)
    {
        if (reader.TokenType == JsonTokenType.Null) return default;

        if (reader.TokenType == JsonTokenType.String)
        {
            var str = reader.GetString()!;
            // Truncar fracción de segundos a 7 dígitos máximo
            var dot = str.IndexOf('.');
            if (dot >= 0)
            {
                int end = dot + 1;
                while (end < str.Length && char.IsDigit(str[end])) end++;
                if (end - dot - 1 > 7)
                    str = str[..(dot + 8)] + str[end..];
            }
            return DateTime.Parse(str, null, System.Globalization.DateTimeStyles.RoundtripKind);
        }

        if (reader.TokenType == JsonTokenType.StartArray)
        {
            // Jackson serializa LocalDateTime como array [año, mes, día, h, m, s, nano]
            var p = new List<int>();
            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                p.Add(reader.GetInt32());

            return new DateTime(
                p[0], p[1], p[2],
                p.Count > 3 ? p[3] : 0,
                p.Count > 4 ? p[4] : 0,
                p.Count > 5 ? p[5] : 0,
                p.Count > 6 ? p[6] / 1_000_000 : 0   // nano → milisegundos
            );
        }

        throw new JsonException($"No se puede convertir el token '{reader.TokenType}' a DateTime.");
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        // Enviamos sin offset (Unspecified) para que Java LocalDateTime lo acepte
        => writer.WriteStringValue(
               DateTime.SpecifyKind(value, DateTimeKind.Unspecified).ToString("s"));
}

/// <summary>
/// Versión nullable del conversor anterior.
/// </summary>
public class FlexibleNullableDateTimeConverter : JsonConverter<DateTime?>
{
    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null) return null;
        return FlexibleDateTimeConverter.ReadCore(ref reader);
    }

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        if (value is null)
            writer.WriteNullValue();
        else
            writer.WriteStringValue(
                DateTime.SpecifyKind(value.Value, DateTimeKind.Unspecified).ToString("s"));
    }
}
