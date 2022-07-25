using System.Text.Json;
using System.Text.Json.Serialization;
using UnitsNet;

namespace OpenWeatherMap.Net.Converters;

internal class MassConcentrationConverter : JsonConverter<MassConcentration>
{
  public override MassConcentration Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    if (!reader.TryGetDouble(out var val)) throw new JsonException();
    return MassConcentration.FromMicrogramsPerCubicMeter(val);
  }

  public override void Write(Utf8JsonWriter writer, MassConcentration value, JsonSerializerOptions options)
  {
    throw new NotImplementedException();
  }
}