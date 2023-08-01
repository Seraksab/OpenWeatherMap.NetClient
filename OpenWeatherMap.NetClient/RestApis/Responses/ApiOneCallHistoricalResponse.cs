using System.Text.Json.Serialization;
using OpenWeatherMap.NetClient.Converters;
using UnitsNet;

namespace OpenWeatherMap.NetClient.RestApis.Responses;

internal sealed class ApiOneCallHistoricalResponse
{
  [JsonPropertyName("lat")] public double Latitude { get; set; }

  [JsonPropertyName("lon")] public double Longitude { get; set; }

  [JsonPropertyName("timezone")] public string TimeZone { get; set; } = null!;

  [JsonPropertyName("timezone_offset"), JsonConverter(typeof(TimeSpanConverter))]
  public TimeSpan TimeZoneOffset { get; set; }

  [JsonPropertyName("data")] public IEnumerable<DataResponse> Data { get; set; } = null!;

  internal sealed class DataResponse
  {
    [JsonPropertyName("dt"), JsonConverter(typeof(DateTimeOffsetConverter))]
    public DateTimeOffset TimeStamp { get; set; }

    [JsonPropertyName("sunrise"), JsonConverter(typeof(DateTimeOffsetConverter))]
    public DateTimeOffset Sunrise { get; set; }

    [JsonPropertyName("sunset"), JsonConverter(typeof(DateTimeOffsetConverter))]
    public DateTimeOffset Sunset { get; set; }

    [JsonPropertyName("temp"), JsonConverter(typeof(TemperatureConverter))]
    public Temperature Temperature { get; set; }

    [JsonPropertyName("feels_like"), JsonConverter(typeof(TemperatureConverter))]
    public Temperature TemperatureFeelsLike { get; set; }

    [JsonPropertyName("pressure"), JsonConverter(typeof(PressureConverter))]
    public Pressure Pressure { get; set; }

    [JsonPropertyName("humidity"), JsonConverter(typeof(HumidityConverter))]
    public RelativeHumidity Humidity { get; set; }

    [JsonPropertyName("clouds"), JsonConverter(typeof(RatioConverter))]
    public Ratio Clouds { get; set; }

    [JsonPropertyName("visibility"), JsonConverter(typeof(LengthMeterConverter))]
    public Length Visibility { get; set; }

    [JsonPropertyName("wind_speed"), JsonConverter(typeof(SpeedConverter))]
    public Speed WindSpeed { get; set; }

    [JsonPropertyName("wind_deg"), JsonConverter(typeof(AngleConverter))]
    public Angle WindDirection { get; set; }

    [JsonPropertyName("wind_gust"), JsonConverter(typeof(SpeedConverter))]
    public Speed? WindGust { get; set; }

    [JsonPropertyName("rain")] public PrecipitationResponse? Rain { get; set; }

    [JsonPropertyName("snow")] public PrecipitationResponse? Snow { get; set; }

    [JsonPropertyName("weather")] public IEnumerable<WeatherResponse> Weather { get; set; } = null!;
  }

  internal sealed class WeatherResponse
  {
    [JsonPropertyName("id")] public int Id { get; set; }

    [JsonPropertyName("main")] public string Main { get; set; } = null!;

    [JsonPropertyName("description")] public string Description { get; set; } = null!;

    [JsonPropertyName("icon")] public string Icon { get; set; } = null!;
  }

  internal sealed class PrecipitationResponse
  {
    [JsonPropertyName("1h"), JsonConverter(typeof(LengthMilliMeterConverter))]
    public Length? OneHour { get; set; }
  }
}