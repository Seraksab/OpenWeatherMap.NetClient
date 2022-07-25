using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenWeatherMap.NetClient.Converters;

internal sealed class DateTimeConverter : JsonConverter<DateTime>
{
  public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    if (!reader.TryGetInt64(out var val)) throw new JsonException();
    return DateTimeOffset.FromUnixTimeSeconds(val).DateTime;
  }

  public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
  {
    throw new NotImplementedException();
  }
}