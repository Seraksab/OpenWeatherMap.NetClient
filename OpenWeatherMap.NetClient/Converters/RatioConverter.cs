using System.Text.Json;
using System.Text.Json.Serialization;
using UnitsNet;

namespace OpenWeatherMap.NetClient.Converters;

internal class RatioConverter : JsonConverter<Ratio>
{
  public override Ratio Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    if (!reader.TryGetDouble(out var val)) throw new JsonException();
    return Ratio.FromPercent(val);
  }

  public override void Write(Utf8JsonWriter writer, Ratio value, JsonSerializerOptions options)
  {
    throw new NotImplementedException();
  }
}