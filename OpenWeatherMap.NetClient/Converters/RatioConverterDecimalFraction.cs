using System.Text.Json;
using System.Text.Json.Serialization;
using UnitsNet;

namespace OpenWeatherMap.NetClient.Converters;

internal class RatioConverterDecimalFraction : JsonConverter<Ratio>
{
  public override Ratio Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    if (!reader.TryGetDouble(out var val)) throw new JsonException();
    return Ratio.FromDecimalFractions(val);
  }

  public override void Write(Utf8JsonWriter writer, Ratio value, JsonSerializerOptions options)
  {
    throw new NotImplementedException();
  }
}