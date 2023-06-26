using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenWeatherMap.NetClient.Converters;

internal sealed class DateTimeOffsetConverter : JsonConverter<DateTimeOffset>
{
  public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    if (!reader.TryGetInt64(out var val)) throw new JsonException();
    return DateTimeOffset.FromUnixTimeSeconds(val);
  }

  public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
  {
    throw new NotImplementedException();
  }
}