using System.Text.Json;
using System.Text.Json.Serialization;
using UnitsNet;

namespace OpenWeatherMap.Net.Converters;

internal class SpeedConverter : JsonConverter<Speed>
{
  public override Speed Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    if (!reader.TryGetDouble(out var val)) throw new JsonException();
    return Speed.FromMetersPerSecond(val);
  }

  public override void Write(Utf8JsonWriter writer, Speed value, JsonSerializerOptions options)
  {
    throw new NotImplementedException();
  }
}