using UnitsNet;

namespace OpenWeatherMap.NetClient.Models;

/// <summary>
/// Aggregated historical weather data
/// </summary>
public sealed class OneCallHistoricalDayWeather
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
  /// Timezone for the requested location in the ±XX:XX format
  /// </summary>
  public string TimeZone { get; internal set; } = null!;

  /// <summary>
  /// Date specified in the API request in the `YYYY-MM-DD` format 
  /// </summary>
  public string Date { get; set; } = null!;

  /// <summary>
  /// Cloudiness at 12:00 for the date specified in the request
  /// </summary>
  public Ratio Clouds { get; internal set; }

  /// <summary>
  /// Humidity at 12:00 for the date specified in the request
  /// </summary>
  public RelativeHumidity Humidity { get; internal set; }

  /// <summary>
  /// Total amount of liquid water equivalent of precipitation for the date specified in the request 
  /// </summary>
  public Length Precipitation { get; internal set; }

  /// <summary>
  /// Atmospheric pressure at 12:00 for the date specified in the request
  /// </summary>
  public Pressure Pressure { get; internal set; }

  /// <summary>
  /// Minimum temperature for the date specified in the request
  /// </summary>
  public Temperature TemperatureMin { get; internal set; }

  /// <summary>
  /// Maximum temperature for the date specified in the request
  /// </summary>
  public Temperature TemperatureMax { get; internal set; }

  /// <summary>
  /// Temperature at 06:00 for the date specified in the request
  /// </summary>
  public Temperature TemperatureMorning { get; internal set; }

  /// <summary>
  /// Temperature at 12:00 for the date specified in the request
  /// </summary>
  public Temperature TemperatureAfternoon { get; internal set; }

  /// <summary>
  /// Temperature at 18:00 for the date specified in the request
  /// </summary>
  public Temperature TemperatureEvening { get; internal set; }

  /// <summary>
  /// Temperature at 00:00 for the date specified in the request
  /// </summary>
  public Temperature TemperatureNight { get; internal set; }

  /// <summary>
  /// Maximum wind speed for the date specified in the request
  /// </summary>
  public Speed WindSpeed { get; internal set; }

  /// <summary>
  /// Wind cardinal direction relevant to the maximum wind speed
  /// </summary>
  public Angle WindDirection { get; internal set; }
}