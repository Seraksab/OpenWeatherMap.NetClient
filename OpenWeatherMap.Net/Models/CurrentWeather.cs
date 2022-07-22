using UnitsNet;

namespace OpenWeatherMap.Net.Models;

public sealed class CurrentWeather
{
  /// <summary>
  /// Time of data calculation
  /// </summary>
  public DateTime MeasuredTimeStamp { get; internal set; }

  /// <summary>
  /// Time the data was fetched from the API
  /// </summary>
  public DateTime FetchedTimeStamp { get; internal set; }

  /// <summary>
  /// City ID
  /// </summary>
  public long CityId { get; internal set; }

  /// <summary>
  /// City name
  /// </summary>
  public string CityName { get; internal set; } = null!;

  /// <summary>
  /// Country code (GB, JP etc.)
  /// </summary>
  public string Country { get; internal set; } = null!;

  /// <summary>
  /// City geo location, longitude
  /// </summary>
  public double Longitude { get; internal set; }

  /// <summary>
  /// City geo location, latitude
  /// </summary>
  public double Latitude { get; internal set; }

  /// <summary>
  /// Shift from UTC
  /// </summary>
  public TimeSpan TimeZoneOffset { get; internal set; }

  /// <summary>
  /// Sunrise time
  /// </summary>
  public DateTime Sunrise { get; internal set; }

  /// <summary>
  /// Sunset time
  /// </summary>
  public DateTime Sunset { get; internal set; }

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

  /// <summary>
  /// Visibility. The maximum value of the visibility is 10km
  /// </summary>
  public Length Visibility { get; internal set; }

  /// <summary>
  /// Temperature
  /// </summary>
  public Temperature Temperature { get; internal set; }

  /// <summary>
  /// Temperature. This temperature parameter accounts for the human perception of weather
  /// </summary>
  public Temperature TemperatureFeelsLike { get; internal set; }

  /// <summary>
  /// Minimum temperature at the moment. This is minimal currently observed temperature (within large megalopolises and urban areas)
  /// </summary>
  public Temperature TemperatureMin { get; internal set; }

  /// <summary>
  /// Maximum temperature at the moment. This is maximal currently observed temperature (within large megalopolises and urban areas)
  /// </summary>
  public Temperature TemperatureMax { get; internal set; }

  /// <summary>
  /// Atmospheric pressure
  /// </summary>
  public Pressure Pressure { get; internal set; }

  /// <summary>
  /// Humidity
  /// </summary>
  public RelativeHumidity Humidity { get; internal set; }

  /// <summary>
  /// Wind speed
  /// </summary>
  public Speed WindSpeed { get; internal set; }

  /// <summary>
  /// Wind direction
  /// </summary>
  public Angle WindDirection { get; internal set; }

  /// <summary>
  /// Cloudiness
  /// </summary>
  public Ratio Cloudiness { get; internal set; }

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
}