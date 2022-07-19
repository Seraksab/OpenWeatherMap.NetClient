using System.Text.Json.Serialization;
using OpenWeatherMap.Net.Converters;
using UnitsNet;

namespace OpenWeatherMap.Net.Models;

public class ApiWeatherResponse
{
  [JsonPropertyName("coord")] public CoordinatesResponse Coordinates { get; set; } = null!;
  [JsonPropertyName("weather")] public IEnumerable<WeatherResponse> Weather { get; set; } = null!;
  [JsonPropertyName("main")] public MainResponse Main { get; set; } = null!;
  [JsonPropertyName("wind")] public WindResponse Wind { get; set; } = null!;
  [JsonPropertyName("clouds")] public CloudsResponse Clouds { get; set; } = null!;
  [JsonPropertyName("rain")] public PrecipitationResponse? Rain { get; set; } = null!;
  [JsonPropertyName("snow")] public PrecipitationResponse? Snow { get; set; } = null!;

  [JsonPropertyName("dt"), JsonConverter(typeof(DateTimeConverter))]
  public DateTime DataTimeStamp { get; set; }

  [JsonPropertyName("sys")] public SysResponse Sys { get; set; } = null!;

  [JsonPropertyName("timezone"), JsonConverter(typeof(TimeSpanConverter))]
  public TimeSpan TimeZoneOffset { get; set; }

  [JsonPropertyName("id")] public long CityId { get; set; }
  [JsonPropertyName("name")] public string CityName { get; set; } = null!;


  public class CoordinatesResponse
  {
    [JsonPropertyName("lon")] public double Longitude { get; set; }
    [JsonPropertyName("lat")] public double Latitude { get; set; }
  }

  public class WeatherResponse
  {
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("main")] public string Main { get; set; } = null!;
    [JsonPropertyName("description")] public string Description { get; set; } = null!;
    [JsonPropertyName("icon")] public string Icon { get; set; } = null!;
  }

  public class MainResponse
  {
    [JsonPropertyName("temp"), JsonConverter(typeof(TemperatureConverter))]
    public Temperature Temperature { get; set; }

    [JsonPropertyName("feels_like"), JsonConverter(typeof(TemperatureConverter))]
    public Temperature TemperatureFeel { get; set; }

    [JsonPropertyName("temp_min"), JsonConverter(typeof(TemperatureConverter))]
    public Temperature TemperatureMin { get; set; }

    [JsonPropertyName("temp_max"), JsonConverter(typeof(TemperatureConverter))]
    public Temperature TemperatureMax { get; set; }

    [JsonPropertyName("pressure"), JsonConverter(typeof(PressureConverter))]
    public Pressure Pressure { get; set; }

    [JsonPropertyName("humidity"), JsonConverter(typeof(HumidityConverter))]
    public RelativeHumidity Humidity { get; set; }
  }

  public class WindResponse
  {
    [JsonPropertyName("speed"), JsonConverter(typeof(SpeedConverter))]
    public Speed Speed { get; set; }

    [JsonPropertyName("deg"), JsonConverter(typeof(AngleConverter))]
    public Angle Degree { get; set; }
  }

  public class CloudsResponse
  {
    [JsonPropertyName("all"), JsonConverter(typeof(RatioConverter))]
    public Ratio All { get; set; }
  }

  public class PrecipitationResponse
  {
    [JsonPropertyName("1h"), JsonConverter(typeof(LengthConverter))]
    public Length OneHour { get; set; }

    [JsonPropertyName("3h"), JsonConverter(typeof(LengthConverter))]
    public Length ThreeHours { get; set; }
  }

  public class SysResponse
  {
    [JsonPropertyName("country")] public string Country { get; set; } = null!;

    [JsonPropertyName("sunrise"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Sunrise { get; set; }

    [JsonPropertyName("sunset"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Sunset { get; set; }
  }
}