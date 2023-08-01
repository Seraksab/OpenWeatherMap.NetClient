using System.Text.Json.Serialization;
using OpenWeatherMap.NetClient.Converters;
using UnitsNet;

namespace OpenWeatherMap.NetClient.RestApis.Responses;

internal sealed class ApiOneCallHistoricalDayResponse
{
  [JsonPropertyName("lat")] public double Latitude { get; set; }

  [JsonPropertyName("lon")] public double Longitude { get; set; }

  [JsonPropertyName("tz")] public string TimeZone { get; set; } = null!;

  [JsonPropertyName("date")] public string Date { get; set; } = null!;

  [JsonPropertyName("cloud_cover")] public CloudCoverResponse CloudCover { get; set; } = null!;

  [JsonPropertyName("humidity")] public HumidityResponse Humidity { get; set; } = null!;

  [JsonPropertyName("precipitation")] public PrecipitationResponse Precipitation { get; set; } = null!;

  [JsonPropertyName("pressure")] public PressureResponse Pressure { get; set; } = null!;

  [JsonPropertyName("temperature")] public TemperatureResponse Temperature { get; set; } = null!;

  [JsonPropertyName("wind")] public WindResponse Wind { get; set; } = null!;

  internal sealed class CloudCoverResponse
  {
    [JsonPropertyName("afternoon"), JsonConverter(typeof(RatioConverter))]
    public Ratio Afternoon { get; set; }
  }

  internal sealed class HumidityResponse
  {
    [JsonPropertyName("afternoon"), JsonConverter(typeof(HumidityConverter))]
    public RelativeHumidity Afternoon { get; set; }
  }

  internal sealed class PrecipitationResponse
  {
    [JsonPropertyName("total"), JsonConverter(typeof(LengthMilliMeterConverter))]
    public Length Total { get; set; }
  }

  internal sealed class PressureResponse
  {
    [JsonPropertyName("afternoon"), JsonConverter(typeof(PressureConverter))]
    public Pressure Afternoon { get; set; }
  }

  internal sealed class TemperatureResponse
  {
    [JsonPropertyName("morning"), JsonConverter(typeof(TemperatureConverter))]
    public Temperature Morning { get; set; }

    [JsonPropertyName("afternoon"), JsonConverter(typeof(TemperatureConverter))]
    public Temperature Afternoon { get; set; }

    [JsonPropertyName("evening"), JsonConverter(typeof(TemperatureConverter))]
    public Temperature Evening { get; set; }

    [JsonPropertyName("night"), JsonConverter(typeof(TemperatureConverter))]
    public Temperature Night { get; set; }

    [JsonPropertyName("min"), JsonConverter(typeof(TemperatureConverter))]
    public Temperature Min { get; set; }

    [JsonPropertyName("max"), JsonConverter(typeof(TemperatureConverter))]
    public Temperature Max { get; set; }
  }

  internal sealed class WindResponse
  {
    [JsonPropertyName("max")] public MaxResponse Max { get; set; } = null!;

    internal sealed class MaxResponse
    {
      [JsonPropertyName("speed"), JsonConverter(typeof(SpeedConverter))]
      public Speed Speed { get; set; }

      [JsonPropertyName("direction"), JsonConverter(typeof(AngleConverter))]
      public Angle Direction { get; set; }
    }
  }
}