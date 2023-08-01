using UnitsNet;

namespace OpenWeatherMap.NetClient.Models;

/// <summary>
/// Current weather, minute forecast for 1 hour, hourly forecast for 48 hours, daily forecast for 8 days and
/// government weather alerts 
/// </summary>
public sealed class OneCallCurrentWeather
{
  /// <summary>
  /// Time the data was fetched from the API
  /// </summary>
  public DateTimeOffset FetchedTimeStamp { get; internal set; }

  /// <summary>
  /// City geo location, longitude
  /// </summary>
  public double Longitude { get; internal set; }

  /// <summary>
  /// City geo location, latitude
  /// </summary>
  public double Latitude { get; internal set; }

  /// <summary>
  /// Timezone name for the requested location
  /// </summary>
  public string TimeZone { get; internal set; } = null!;

  /// <summary>
  /// Shift from UTC
  /// </summary>
  public TimeSpan TimeZoneOffset { get; internal set; }

  /// <summary>
  /// Current weather data
  /// </summary>
  public CurrentWeather? Current { get; internal set; }

  /// <summary>
  /// Minute forecast weather data
  /// </summary>
  public IEnumerable<MinuteForecast> Minutely { get; internal set; } = null!;

  /// <summary>
  /// Hourly forecast weather data
  /// </summary>
  public IEnumerable<HourForecast> Hourly { get; internal set; } = null!;

  /// <summary>
  /// Daily forecast weather data
  /// </summary>
  public IEnumerable<DayForecast> Daily { get; internal set; } = null!;

  /// <summary>
  /// National weather alerts data from major national weather warning systems
  /// </summary>
  public IEnumerable<Alert> Alerts { get; internal set; } = null!;

  /// <summary>
  /// Current weather data
  /// </summary>
  public sealed class CurrentWeather
  {
    /// <summary>
    /// Time of data calculation
    /// </summary>
    public DateTimeOffset MeasuredTimeStamp { get; internal set; }

    /// <summary>
    /// Sunrise time
    /// </summary>
    public DateTimeOffset Sunrise { get; internal set; }

    /// <summary>
    /// Sunset time
    /// </summary>
    public DateTimeOffset Sunset { get; internal set; }

    /// <summary>
    /// Temperature
    /// </summary>
    public Temperature Temperature { get; internal set; }

    /// <summary>
    /// Temperature. This temperature parameter accounts for the human perception of weather
    /// </summary>
    public Temperature TemperatureFeelsLike { get; internal set; }

    /// <summary>
    /// Atmospheric pressure on the sea level
    /// </summary>
    public Pressure Pressure { get; internal set; }

    /// <summary>
    /// Humidity
    /// </summary>
    public RelativeHumidity Humidity { get; internal set; }

    /// <summary>
    /// Cloudiness
    /// </summary>
    public Ratio Clouds { get; internal set; }

    /// <summary>
    /// Visibility. The maximum value of the visibility is 10km
    /// </summary>
    public Length Visibility { get; internal set; }

    /// <summary>
    /// Wind speed
    /// </summary>
    public Speed WindSpeed { get; internal set; }

    /// <summary>
    /// Wind direction
    /// </summary>
    public Angle WindDirection { get; internal set; }

    /// <summary>
    /// Wind gust
    /// </summary>
    public Speed? WindGust { get; internal set; }

    /// <summary>
    /// Rain volume for the last 1 hour
    /// </summary>
    public Length? RainLastHour { get; internal set; }

    /// <summary>
    /// Rain volume for the last 3 hour
    /// </summary>
    public Length? RainLastThreeHours { get; internal set; }

    /// <summary>
    /// Snow volume for the last 1 hour
    /// </summary>
    public Length? SnowLastHour { get; internal set; }

    /// <summary>
    ///  Snow volume for the last 3 hours
    /// </summary>
    public Length? SnowLastThreeHours { get; internal set; }

    /// <summary>
    /// Weather condition id
    /// </summary>
    public int WeatherConditionId { get; internal set; }

    /// <summary>
    /// Group of weather parameters (Rain, Snow, Extreme etc.)
    /// </summary>
    public string WeatherCondition { get; internal set; } = null!;

    /// <summary>
    /// Weather condition within the group
    /// </summary>
    public string WeatherDescription { get; internal set; } = null!;

    /// <summary>
    /// Weather icon id
    /// </summary>
    public string WeatherIcon { get; internal set; } = null!;
  }

  /// <summary>
  /// Weather alert
  /// </summary>
  public sealed class Alert
  {
    /// <summary>
    /// Name of the alert source 
    /// </summary>
    public string Sender { get; internal set; } = null!;

    /// <summary>
    /// Alert event name
    /// </summary>
    public string Event { get; internal set; } = null!;

    /// <summary>
    /// Description of the alert
    /// </summary>
    public string Description { get; internal set; } = null!;

    /// <summary>
    /// Date and time of the start of the alert
    /// </summary>
    public DateTimeOffset Start { get; internal set; }

    /// <summary>
    /// Date and time of the end of the alert
    /// </summary>
    public DateTimeOffset End { get; internal set; }
  }

  /// <summary>
  /// Minute forecast weather data
  /// </summary>
  public sealed class MinuteForecast
  {
    /// <summary>
    /// Time of the forecasted data 
    /// </summary>
    public DateTimeOffset TimeStamp { get; internal set; }

    /// <summary>
    /// Precipitation
    /// </summary>
    public Length Precipitation { get; internal set; }
  }

  /// <summary>
  /// Hourly forecast weather data
  /// </summary>
  public sealed class HourForecast
  {
    /// <summary>
    /// Time of the forecasted data 
    /// </summary>
    public DateTimeOffset TimeStamp { get; internal set; }

    /// <summary>
    /// Temperature
    /// </summary>
    public Temperature Temperature { get; internal set; }

    /// <summary>
    /// Temperature. This temperature parameter accounts for the human perception of weather
    /// </summary>
    public Temperature TemperatureFeelsLike { get; internal set; }

    /// <summary>
    /// Atmospheric pressure on the sea level
    /// </summary>
    public Pressure Pressure { get; internal set; }

    /// <summary>
    /// Humidity
    /// </summary>
    public RelativeHumidity Humidity { get; internal set; }

    /// <summary>
    /// Cloudiness
    /// </summary>
    public Ratio Clouds { get; internal set; }

    /// <summary>
    /// Visibility. The maximum value of the visibility is 10km
    /// </summary>
    public Length Visibility { get; internal set; }

    /// <summary>
    /// Wind speed
    /// </summary>
    public Speed WindSpeed { get; internal set; }

    /// <summary>
    /// Wind direction
    /// </summary>
    public Angle WindDirection { get; internal set; }

    /// <summary>
    /// Wind gust
    /// </summary>
    public Speed? WindGust { get; internal set; }

    /// <summary>
    /// Probability of precipitation
    /// </summary>
    public Ratio ProbabilityPrecipitation { get; set; }

    /// <summary>
    /// Rain amount
    /// </summary>
    public Length? Rain { get; internal set; }

    /// <summary>
    /// Snow amount
    /// </summary>
    public Length? Snow { get; internal set; }

    /// <summary>
    /// Weather condition id
    /// </summary>
    public int WeatherConditionId { get; internal set; }

    /// <summary>
    /// Group of weather parameters (Rain, Snow, Extreme etc.)
    /// </summary>
    public string WeatherCondition { get; internal set; } = null!;

    /// <summary>
    /// Weather condition within the group
    /// </summary>
    public string WeatherDescription { get; internal set; } = null!;

    /// <summary>
    /// Weather icon id
    /// </summary>
    public string WeatherIcon { get; internal set; } = null!;
  }

  /// <summary>
  /// Daily forecast weather data
  /// </summary>
  public sealed class DayForecast
  {
    /// <summary>
    /// Time of the forecasted data 
    /// </summary>
    public DateTimeOffset TimeStamp { get; internal set; }

    /// <summary>
    /// Sunrise time
    /// </summary>
    public DateTimeOffset Sunrise { get; internal set; }

    /// <summary>
    /// Sunset time
    /// </summary>
    public DateTimeOffset Sunset { get; internal set; }

    /// <summary>
    /// Moonrise time
    /// </summary>
    public DateTimeOffset Moonrise { get; internal set; }

    /// <summary>
    /// Moonset time
    /// </summary>
    public DateTimeOffset Moonset { get; internal set; }

    /// <summary>
    /// Human-readable description of the weather conditions for the day
    /// </summary>
    public string Summary { get; internal set; } = null!;

    /// <summary>
    /// Morning temperature
    /// </summary>
    public Temperature TemperatureMorning { get; internal set; }

    /// <summary>
    /// Day temperature
    /// </summary>
    public Temperature TemperatureDay { get; internal set; }

    /// <summary>
    /// Evening temperature
    /// </summary>
    public Temperature TemperatureEvening { get; internal set; }

    /// <summary>
    /// Night temperature
    /// </summary>
    public Temperature TemperatureNight { get; internal set; }

    /// <summary>
    /// Min daily temperature
    /// </summary>
    public Temperature TemperatureMin { get; internal set; }

    /// <summary>
    /// Max daily temperature
    /// </summary>
    public Temperature TemperatureMax { get; internal set; }

    /// <summary>
    /// Morning Temperature. This temperature parameter accounts for the human perception of weather
    /// </summary>
    public Temperature TemperatureMorningFeelsLike { get; internal set; }

    /// <summary>
    /// Day Temperature. This temperature parameter accounts for the human perception of weather
    /// </summary>
    public Temperature TemperatureDayFeelsLike { get; internal set; }

    /// <summary>
    /// Evening Temperature. This temperature parameter accounts for the human perception of weather
    /// </summary>
    public Temperature TemperatureEveningFeelsLike { get; internal set; }

    /// <summary>
    /// Night Temperature. This temperature parameter accounts for the human perception of weather
    /// </summary>
    public Temperature TemperatureNightFeelsLike { get; internal set; }

    /// <summary>
    /// Atmospheric pressure on the sea level
    /// </summary>
    public Pressure Pressure { get; internal set; }

    /// <summary>
    /// Humidity
    /// </summary>
    public RelativeHumidity Humidity { get; internal set; }

    /// <summary>
    /// Cloudiness
    /// </summary>
    public Ratio Clouds { get; internal set; }

    /// <summary>
    /// Wind speed
    /// </summary>
    public Speed WindSpeed { get; internal set; }

    /// <summary>
    /// Wind direction
    /// </summary>
    public Angle WindDirection { get; internal set; }

    /// <summary>
    /// Wind gust
    /// </summary>
    public Speed? WindGust { get; internal set; }

    /// <summary>
    /// Probability of precipitation
    /// </summary>
    public Ratio ProbabilityPrecipitation { get; set; }

    /// <summary>
    /// Rain amount
    /// </summary>
    public Length? Rain { get; internal set; }

    /// <summary>
    /// Snow amount
    /// </summary>
    public Length? Snow { get; internal set; }

    /// <summary>
    /// Weather condition id
    /// </summary>
    public int WeatherConditionId { get; internal set; }

    /// <summary>
    /// Group of weather parameters (Rain, Snow, Extreme etc.)
    /// </summary>
    public string WeatherCondition { get; internal set; } = null!;

    /// <summary>
    /// Weather condition within the group
    /// </summary>
    public string WeatherDescription { get; internal set; } = null!;

    /// <summary>
    /// Weather icon id
    /// </summary>
    public string WeatherIcon { get; internal set; } = null!;
  }
}