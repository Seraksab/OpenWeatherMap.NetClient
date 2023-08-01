using System.Text.Json.Serialization;
using OpenWeatherMap.NetClient.Converters;
using UnitsNet;

namespace OpenWeatherMap.NetClient.RestApis.Responses;

internal sealed class ApiOneCallCurrentResponse
{
  [JsonPropertyName("lat")] public double Latitude { get; set; }

  [JsonPropertyName("lon")] public double Longitude { get; set; }

  [JsonPropertyName("timezone")] public string TimeZone { get; set; } = null!;

  [JsonPropertyName("timezone_offset"), JsonConverter(typeof(TimeSpanConverter))]
  public TimeSpan TimeZoneOffset { get; set; }

  [JsonPropertyName("current")] public CurrentResponse? Current { get; set; }

  [JsonPropertyName("minutely")] public IEnumerable<MinuteForecastResponse>? MinuteForecast { get; set; }

  [JsonPropertyName("hourly")] public IEnumerable<HourForecastResponse>? HourForecast { get; set; }

  [JsonPropertyName("daily")] public IEnumerable<DayForecastResponse>? DayForecast { get; set; }

  [JsonPropertyName("alerts")]
  public IEnumerable<AlertResponse> Alerts { get; set; } = Enumerable.Empty<AlertResponse>();

  internal sealed class CurrentResponse
  {
    [JsonPropertyName("dt"), JsonConverter(typeof(DateTimeOffsetConverter))]
    public DateTimeOffset DataTimeStamp { get; set; }

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

    [JsonPropertyName("rain")] public PrecipitationResponse? Rain { get; set; } = null!;

    [JsonPropertyName("snow")] public PrecipitationResponse? Snow { get; set; } = null!;

    [JsonPropertyName("weather")] public IEnumerable<WeatherResponse> Weather { get; set; } = null!;
  }

  internal sealed class WeatherResponse
  {
    [JsonPropertyName("id")] public int Id { get; set; }

    [JsonPropertyName("main")] public string Main { get; set; } = null!;

    [JsonPropertyName("description")] public string Description { get; set; } = null!;

    [JsonPropertyName("icon")] public string Icon { get; set; } = null!;
  }

  internal sealed class AlertResponse
  {
    [JsonPropertyName("sender_name")] public string SenderName { get; set; } = null!;

    [JsonPropertyName("event")] public string Event { get; set; } = null!;

    [JsonPropertyName("start"), JsonConverter(typeof(DateTimeOffsetConverter))]
    public DateTimeOffset Start { get; set; }

    [JsonPropertyName("end"), JsonConverter(typeof(DateTimeOffsetConverter))]
    public DateTimeOffset End { get; set; }

    [JsonPropertyName("description")] public string Description { get; set; } = null!;
  }

  internal sealed class PrecipitationResponse
  {
    [JsonPropertyName("1h"), JsonConverter(typeof(LengthMilliMeterConverter))]
    public Length? OneHour { get; set; }

    [JsonPropertyName("3h"), JsonConverter(typeof(LengthMilliMeterConverter))]
    public Length? ThreeHours { get; set; }
  }

  internal sealed class MinuteForecastResponse
  {
    [JsonPropertyName("dt"), JsonConverter(typeof(DateTimeOffsetConverter))]
    public DateTimeOffset TimeStamp { get; set; }

    [JsonPropertyName("precipitation "), JsonConverter(typeof(LengthMilliMeterConverter))]
    public Length Precipitation { get; set; }
  }

  internal sealed class HourForecastResponse
  {
    [JsonPropertyName("dt"), JsonConverter(typeof(DateTimeOffsetConverter))]
    public DateTimeOffset TimeStamp { get; set; }

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

    [JsonPropertyName("pop"), JsonConverter(typeof(RatioConverterDecimalFraction))]
    public Ratio ProbabilityPrecipitation { get; set; }

    [JsonPropertyName("rain")] public PrecipitationResponse? Rain { get; set; }

    [JsonPropertyName("snow")] public PrecipitationResponse? Snow { get; set; }

    [JsonPropertyName("weather")] public IEnumerable<WeatherResponse> Weather { get; set; } = null!;
  }

  internal sealed class DayForecastResponse
  {
    [JsonPropertyName("dt"), JsonConverter(typeof(DateTimeOffsetConverter))]
    public DateTimeOffset TimeStamp { get; set; }

    [JsonPropertyName("sunrise"), JsonConverter(typeof(DateTimeOffsetConverter))]
    public DateTimeOffset Sunrise { get; set; }

    [JsonPropertyName("sunset"), JsonConverter(typeof(DateTimeOffsetConverter))]
    public DateTimeOffset Sunset { get; set; }

    [JsonPropertyName("moonrise"), JsonConverter(typeof(DateTimeOffsetConverter))]
    public DateTimeOffset Moonrise { get; set; }

    [JsonPropertyName("moonset"), JsonConverter(typeof(DateTimeOffsetConverter))]
    public DateTimeOffset MoonSet { get; set; }

    [JsonPropertyName("summary")] public string Summary { get; set; } = null!;

    [JsonPropertyName("temp")] public DayForecastTemperatureResponse Temperature { get; set; } = null!;

    [JsonPropertyName("feels_like")] public DayForecastTemperatureResponse TemperatureFeelsLike { get; set; } = null!;

    [JsonPropertyName("pressure"), JsonConverter(typeof(PressureConverter))]
    public Pressure Pressure { get; set; }

    [JsonPropertyName("humidity"), JsonConverter(typeof(HumidityConverter))]
    public RelativeHumidity Humidity { get; set; }

    [JsonPropertyName("clouds"), JsonConverter(typeof(RatioConverter))]
    public Ratio Clouds { get; set; }

    [JsonPropertyName("wind_speed"), JsonConverter(typeof(SpeedConverter))]
    public Speed WindSpeed { get; set; }

    [JsonPropertyName("wind_deg"), JsonConverter(typeof(AngleConverter))]
    public Angle WindDirection { get; set; }

    [JsonPropertyName("wind_gust"), JsonConverter(typeof(SpeedConverter))]
    public Speed? WindGust { get; set; }

    [JsonPropertyName("pop"), JsonConverter(typeof(RatioConverterDecimalFraction))]
    public Ratio ProbabilityPrecipitation { get; set; }

    [JsonPropertyName("rain"), JsonConverter(typeof(LengthMilliMeterConverter))]
    public Length? Rain { get; set; }

    [JsonPropertyName("snow"), JsonConverter(typeof(LengthMilliMeterConverter))]
    public Length? Snow { get; set; }

    [JsonPropertyName("weather")] public IEnumerable<WeatherResponse> Weather { get; set; } = null!;
  }

  internal sealed class DayForecastTemperatureResponse
  {
    [JsonPropertyName("morn"), JsonConverter(typeof(TemperatureConverter))]
    public Temperature Morning { get; set; }

    [JsonPropertyName("day"), JsonConverter(typeof(TemperatureConverter))]
    public Temperature Day { get; set; }

    [JsonPropertyName("eve"), JsonConverter(typeof(TemperatureConverter))]
    public Temperature Evening { get; set; }

    [JsonPropertyName("night"), JsonConverter(typeof(TemperatureConverter))]
    public Temperature Night { get; set; }

    [JsonPropertyName("min"), JsonConverter(typeof(TemperatureConverter))]
    public Temperature? Min { get; set; }

    [JsonPropertyName("max"), JsonConverter(typeof(TemperatureConverter))]
    public Temperature? Max { get; set; }
  }
}