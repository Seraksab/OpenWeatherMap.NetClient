﻿using System.Text.Json.Serialization;
using OpenWeatherMap.NetClient.Converters;
using UnitsNet;

namespace OpenWeatherMap.NetClient.RestApis.Responses;

internal sealed class ApiWeatherResponse
{
  [JsonPropertyName("coord")] public CoordinatesResponse Coordinates { get; set; } = null!;
  [JsonPropertyName("weather")] public IEnumerable<WeatherResponse> Weather { get; set; } = null!;
  [JsonPropertyName("main")] public MainResponse Main { get; set; } = null!;

  [JsonPropertyName("visibility"), JsonConverter(typeof(LengthMeterConverter))]
  public Length Visibility { get; set; }

  [JsonPropertyName("wind")] public WindResponse Wind { get; set; } = null!;
  [JsonPropertyName("clouds")] public CloudsResponse Clouds { get; set; } = null!;
  [JsonPropertyName("rain")] public PrecipitationResponse? Rain { get; set; } = null!;
  [JsonPropertyName("snow")] public PrecipitationResponse? Snow { get; set; } = null!;

  [JsonPropertyName("dt"), JsonConverter(typeof(DateTimeOffsetConverter))]
  public DateTimeOffset DataTimeStamp { get; set; }

  [JsonPropertyName("sys")] public SysResponse Sys { get; set; } = null!;

  [JsonPropertyName("timezone"), JsonConverter(typeof(TimeSpanConverter))]
  public TimeSpan TimeZoneOffset { get; set; }

  [JsonPropertyName("id")] public long CityId { get; set; }
  [JsonPropertyName("name")] public string CityName { get; set; } = null!;


  internal sealed class CoordinatesResponse
  {
    [JsonPropertyName("lon")] public double Longitude { get; set; }
    [JsonPropertyName("lat")] public double Latitude { get; set; }
  }

  internal sealed class WeatherResponse
  {
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("main")] public string Main { get; set; } = null!;
    [JsonPropertyName("description")] public string Description { get; set; } = null!;
    [JsonPropertyName("icon")] public string Icon { get; set; } = null!;
  }

  internal sealed class MainResponse
  {
    [JsonPropertyName("temp"), JsonConverter(typeof(TemperatureConverter))]
    public Temperature Temperature { get; set; }

    [JsonPropertyName("feels_like"), JsonConverter(typeof(TemperatureConverter))]
    public Temperature TemperatureFeelsLike { get; set; }

    [JsonPropertyName("temp_min"), JsonConverter(typeof(TemperatureConverter))]
    public Temperature TemperatureMin { get; set; }

    [JsonPropertyName("temp_max"), JsonConverter(typeof(TemperatureConverter))]
    public Temperature TemperatureMax { get; set; }

    [JsonPropertyName("pressure"), JsonConverter(typeof(PressureConverter))]
    public Pressure Pressure { get; set; }

    [JsonPropertyName("sea_level"), JsonConverter(typeof(PressureConverter))]
    public Pressure PressureSeaLevel { get; set; }

    [JsonPropertyName("grnd_level"), JsonConverter(typeof(PressureConverter))]
    public Pressure PressureGroundLevel { get; set; }

    [JsonPropertyName("humidity"), JsonConverter(typeof(HumidityConverter))]
    public RelativeHumidity Humidity { get; set; }
  }

  internal sealed class WindResponse
  {
    [JsonPropertyName("speed"), JsonConverter(typeof(SpeedConverter))]
    public Speed Speed { get; set; }

    [JsonPropertyName("deg"), JsonConverter(typeof(AngleConverter))]
    public Angle Direction { get; set; }

    [JsonPropertyName("gust"), JsonConverter(typeof(SpeedConverter))]
    public Speed Gust { get; set; }
  }

  internal sealed class CloudsResponse
  {
    [JsonPropertyName("all"), JsonConverter(typeof(RatioConverter))]
    public Ratio All { get; set; }
  }

  internal sealed class PrecipitationResponse
  {
    [JsonPropertyName("1h"), JsonConverter(typeof(LengthMilliMeterConverter))]
    public Length OneHour { get; set; }

    [JsonPropertyName("3h"), JsonConverter(typeof(LengthMilliMeterConverter))]
    public Length ThreeHours { get; set; }
  }

  internal sealed class SysResponse
  {
    [JsonPropertyName("country")] public string Country { get; set; } = null!;

    [JsonPropertyName("sunrise"), JsonConverter(typeof(DateTimeOffsetConverter))]
    public DateTimeOffset Sunrise { get; set; }

    [JsonPropertyName("sunset"), JsonConverter(typeof(DateTimeOffsetConverter))]
    public DateTimeOffset Sunset { get; set; }
  }
}