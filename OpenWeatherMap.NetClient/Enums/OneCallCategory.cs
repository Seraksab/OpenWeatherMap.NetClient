namespace OpenWeatherMap.NetClient.Enums;

/// <summary>
/// Parts of the One Call weather data
/// </summary>
public enum OneCallCategory
{
  /// <summary>
  /// Current weather data 
  /// </summary>
  Current,

  /// <summary>
  ///  Minute forecast weather data
  /// </summary>
  Minutely,

  /// <summary>
  /// Hourly forecast weather data
  /// </summary>
  Hourly,

  /// <summary>
  /// Daily forecast weather data
  /// </summary>
  Daily,

  /// <summary>
  /// National weather alerts data
  /// </summary>
  Alerts
}