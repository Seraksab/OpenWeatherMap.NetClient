namespace OpenWeatherMap.NetClient.Enums;

/// <summary>
/// Air Quality Index (AQI) used to communicate the current or forecasted air quality. 
/// </summary>
/// <remarks>
/// Scale: 1 (best) - 5 (worst)
/// </remarks>
public enum AirQualityIndex
{
  /// <summary>
  /// Good Air Quality
  /// </summary>
  Good = 1,

  /// <summary>
  /// Fair Air Quality
  /// </summary>
  Fair = 2,

  /// <summary>
  /// Moderate Air Quality
  /// </summary>
  Moderate = 3,

  /// <summary>
  /// Poor Air Quality
  /// </summary>
  Poor = 4,

  /// <summary>
  /// Very Poor Air Quality
  /// </summary>
  VeryPoor = 5
}