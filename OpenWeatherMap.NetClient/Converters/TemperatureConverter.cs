using System.Text.Json;
using System.Text.Json.Serialization;
using UnitsNet;

namespace OpenWeatherMap.NetClient.Converters;

internal class TemperatureConverter : JsonConverter<Temperature>
{
  public override Temperature Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    if (!reader.TryGetDouble(out var val)) throw new JsonException();
    return Temperature.FromKelvins(val);
  }

  public override void Write(Utf8JsonWriter writer, Temperature value, JsonSerializerOptions options)
  {
    throw new NotImplementedException();
  }
}