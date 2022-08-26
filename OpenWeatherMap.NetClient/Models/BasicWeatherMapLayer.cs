namespace OpenWeatherMap.NetClient.Models;

/// <summary>
/// Weather map layers available from the <see cref="Apis.IBasicWeatherMapsApi"/> 
/// </summary>
public enum BasicWeatherMapLayer
{
  /// <summary>
  /// Cloud layer
  /// </summary>
  Clouds,

  /// <summary>
  /// Precipitation layer
  /// </summary>
  Precipitation,

  /// <summary>
  /// Sea level pressure layer
  /// </summary>
  SeaLevelPressure,

  /// <summary>
  /// Wind speed layer
  /// </summary>
  WindSpeed,

  /// <summary>
  /// Temperature layer 
  /// </summary>
  Temperature
}