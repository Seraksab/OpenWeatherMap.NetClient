using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenWeatherMap.NetClient.Converters;

internal class TimeSpanConverter : JsonConverter<TimeSpan>
{
  public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    if (!reader.TryGetInt64(out var val)) throw new JsonException();
    return TimeSpan.FromSeconds(val);
  }

  public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
  {
    throw new NotImplementedException();
  }
}