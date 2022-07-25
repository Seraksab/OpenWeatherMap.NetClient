using System.Text.Json.Serialization;
using OpenWeatherMap.Net.Enums;
using UnitsNet;
using DateTimeConverter = OpenWeatherMap.Net.Converters.DateTimeConverter;

namespace OpenWeatherMap.Net.Models.ApiResponses;

internal sealed class ApiAirPollutionResponse
{
  [JsonPropertyName("coord")] public string[] Coordinates { get; set; } = null!;
  [JsonPropertyName("list")] public Element[] Elements { get; set; } = null!;


  internal sealed class Element
  {
    [JsonPropertyName("dt"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime TimeStamp { get; set; }

    [JsonPropertyName("main")] public MainResponse Main { get; set; } = null!;
    [JsonPropertyName("components")] public ComponentsResponse Components { get; set; } = null!;
  }

  internal sealed class MainResponse
  {
    [JsonPropertyName("aqi")] public AirQualityIndex AirQualityIndex { get; set; }
  }

  internal sealed class ComponentsResponse
  {
    [JsonPropertyName("co"), JsonConverter(typeof(MassConcentration))]
    public MassConcentration CarbonMonoxide { get; set; }

    [JsonPropertyName("no"), JsonConverter(typeof(MassConcentration))]
    public MassConcentration NitrogenMonoxide { get; set; }

    [JsonPropertyName("no2"), JsonConverter(typeof(MassConcentration))]
    public MassConcentration NitrogenDioxide { get; set; }

    [JsonPropertyName("o3"), JsonConverter(typeof(MassConcentration))]
    public MassConcentration Ozone { get; set; }

    [JsonPropertyName("so2"), JsonConverter(typeof(MassConcentration))]
    public MassConcentration SulphurDioxide { get; set; }

    [JsonPropertyName("pm2_5"), JsonConverter(typeof(MassConcentration))]
    public MassConcentration FineParticlesMatter { get; set; }

    [JsonPropertyName("pm10"), JsonConverter(typeof(MassConcentration))]
    public MassConcentration CoarseParticulateMatter { get; set; }

    [JsonPropertyName("nh3"), JsonConverter(typeof(MassConcentration))]
    public MassConcentration Ammonia { get; set; }
  }
}