using System.Text.Json.Serialization;
using OpenWeatherMap.NetClient.Converters;
using OpenWeatherMap.NetClient.Enums;
using UnitsNet;
using DateTimeConverter = OpenWeatherMap.NetClient.Converters.DateTimeConverter;

namespace OpenWeatherMap.NetClient.RestApis.Responses;

internal sealed class ApiAirPollutionResponse
{
  [JsonPropertyName("coord")] public CoordinatesResponse Coordinates { get; set; } = null!;
  [JsonPropertyName("list")] public ListElementResponse[] List { get; set; } = null!;

  internal sealed class CoordinatesResponse
  {
    [JsonPropertyName("lon")] public double Longitude { get; set; }
    [JsonPropertyName("lat")] public double Latitude { get; set; }
  }

  internal sealed class ListElementResponse
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
    [JsonPropertyName("co"), JsonConverter(typeof(MassConcentrationConverter))]
    public MassConcentration CarbonMonoxide { get; set; }

    [JsonPropertyName("no"), JsonConverter(typeof(MassConcentrationConverter))]
    public MassConcentration NitrogenMonoxide { get; set; }

    [JsonPropertyName("no2"), JsonConverter(typeof(MassConcentrationConverter))]
    public MassConcentration NitrogenDioxide { get; set; }

    [JsonPropertyName("o3"), JsonConverter(typeof(MassConcentrationConverter))]
    public MassConcentration Ozone { get; set; }

    [JsonPropertyName("so2"), JsonConverter(typeof(MassConcentrationConverter))]
    public MassConcentration SulphurDioxide { get; set; }

    [JsonPropertyName("pm2_5"), JsonConverter(typeof(MassConcentrationConverter))]
    public MassConcentration FineParticlesMatter { get; set; }

    [JsonPropertyName("pm10"), JsonConverter(typeof(MassConcentrationConverter))]
    public MassConcentration CoarseParticulateMatter { get; set; }

    [JsonPropertyName("nh3"), JsonConverter(typeof(MassConcentrationConverter))]
    public MassConcentration Ammonia { get; set; }
  }
}