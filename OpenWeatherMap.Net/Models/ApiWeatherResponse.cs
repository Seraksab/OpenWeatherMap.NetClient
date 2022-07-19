using System.Text.Json.Serialization;

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

  [JsonPropertyName("dt")] public long DataTimeStamp { get; set; }

  [JsonPropertyName("sys")] public SysResponse Sys { get; set; } = null!;

  [JsonPropertyName("timezone")] public long TimeZoneOffset { get; set; }

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
    [JsonPropertyName("temp")] public double Temperature { get; set; }

    [JsonPropertyName("feels_like")] public double TemperatureFeel { get; set; }

    [JsonPropertyName("temp_min")] public double TemperatureMin { get; set; }

    [JsonPropertyName("temp_max")] public double TemperatureMax { get; set; }

    [JsonPropertyName("pressure")] public double Pressure { get; set; }

    [JsonPropertyName("humidity")] public double Humidity { get; set; }
  }

  public class WindResponse
  {
    [JsonPropertyName("speed")] public double Speed { get; set; }

    [JsonPropertyName("deg")] public double Degree { get; set; }
  }

  public class CloudsResponse
  {
    [JsonPropertyName("all")] public double All { get; set; }
  }

  public class PrecipitationResponse
  {
    [JsonPropertyName("1h")] public double OneHour { get; set; }

    [JsonPropertyName("3h")] public double ThreeHours { get; set; }
  }

  public class SysResponse
  {
    [JsonPropertyName("country")] public string Country { get; set; } = null!;

    [JsonPropertyName("sunrise")] public long Sunrise { get; set; }

    [JsonPropertyName("sunset")] public long Sunset { get; set; }
  }
}