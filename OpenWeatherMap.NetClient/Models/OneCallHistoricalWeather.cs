using UnitsNet;

namespace OpenWeatherMap.NetClient.Models;

/// <summary>
/// Historical weather data
/// </summary>
public sealed class OneCallHistoricalWeather
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
  /// Requested time
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
  /// Rain volume for 1 hour
  /// </summary>
  public Length? Rain { get; internal set; }

  /// <summary>
  /// Snow volume for 1 hour
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