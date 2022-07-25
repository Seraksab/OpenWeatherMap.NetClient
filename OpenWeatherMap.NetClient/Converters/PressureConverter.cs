using System.Text.Json;
using System.Text.Json.Serialization;
using UnitsNet;

namespace OpenWeatherMap.NetClient.Converters;

internal class PressureConverter : JsonConverter<Pressure>
{
  public override Pressure Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    if (!reader.TryGetDouble(out var val)) throw new JsonException();
    return Pressure.FromHectopascals(val);
  }

  public override void Write(Utf8JsonWriter writer, Pressure value, JsonSerializerOptions options)
  {
    throw new NotImplementedException();
  }
}