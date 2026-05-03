using Ganss.Xss;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SS.AuthService.API.Configurations.Json;

/// <summary>
/// Converter untuk membersihkan string dari karakter berbahaya (XSS) secara otomatis
/// saat proses deserialisasi JSON (native performance).
/// </summary>
public class SanitizedStringConverter : JsonConverter<string>
{
    private static readonly HtmlSanitizer Sanitizer = new();

    static SanitizedStringConverter()
    {
        // Secara default HtmlSanitizer menghapus semua tag. 
        // Untuk API, kita biasanya ingin membersihkan string mentah.
        Sanitizer.AllowedTags.Clear();
        Sanitizer.AllowedAttributes.Clear();
    }

    public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        if (string.IsNullOrEmpty(value)) return value;

        // Sanitasi menggunakan library yang battle-tested
        return Sanitizer.Sanitize(value);
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value);
    }
}
