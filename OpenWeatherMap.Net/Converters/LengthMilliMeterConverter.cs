using System.Text.Json;
using System.Text.Json.Serialization;
using UnitsNet;

namespace OpenWeatherMap.Net.Converters;

internal class LengthMilliMeterConverter : JsonConverter<Length>
{
  public override Length Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    if (!reader.TryGetDouble(out var val)) throw new JsonException();
    return Length.FromMillimeters(val);
  }

  public override void Write(Utf8JsonWriter writer, Length value, JsonSerializerOptions options)
  {
    throw new NotImplementedException();
  }
}