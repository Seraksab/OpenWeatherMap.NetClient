using System.Text.Json;
using System.Text.Json.Serialization;
using UnitsNet;

namespace OpenWeatherMap.Net.Converters;

internal class HumidityConverter : JsonConverter<RelativeHumidity>
{
  public override RelativeHumidity Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    if (!reader.TryGetDouble(out var val)) throw new JsonException();
    return RelativeHumidity.FromPercent(val);
  }

  public override void Write(Utf8JsonWriter writer, RelativeHumidity value, JsonSerializerOptions options)
  {
    throw new NotImplementedException();
  }
}