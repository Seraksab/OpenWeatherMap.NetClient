using System.Text.Json;
using System.Text.Json.Serialization;
using UnitsNet;

namespace OpenWeatherMap.NetClient.Converters;

internal class AngleConverter : JsonConverter<Angle>
{
  public override Angle Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    if (!reader.TryGetDouble(out var val)) throw new JsonException();
    return Angle.FromDegrees(val);
  }

  public override void Write(Utf8JsonWriter writer, Angle value, JsonSerializerOptions options)
  {
    throw new NotImplementedException();
  }
}