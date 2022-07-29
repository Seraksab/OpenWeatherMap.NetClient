using OpenWeatherMap.NetClient.Apis;

namespace OpenWeatherMap.NetClient;

/// <summary>
/// Access to different OpenWeatherMap APIs
/// </summary>
public interface IOpenWeatherMap
{
  /// <summary>
  /// Access to the 'Current Weather Data API'
  /// </summary>
  /// <remarks>
  /// The 'Current Weather Data API' provides current weather data for any location on Earth including over 200,000 cities.
  /// Data is collected and processed from different sources such as global and local weather models, satellites, radars and
  /// a vast network of weather stations.
  /// </remarks>
  /// <returns>An implementation of <see cref="ICurrentWeatherApi"/></returns>
  ICurrentWeatherApi CurrentWeather { get; }

  /// <summary>
  /// Access to the 'Geocoding API'
  /// </summary>
  /// <remarks>
  /// The 'Geocoding API' is a simple tool developed to ease the search for locations while working with geographic
  /// names and coordinates.
  /// </remarks>
  /// <returns>An implementation of <see cref="IGeocodingApi"/></returns>
  IGeocodingApi Geocoding { get; }

  /// <summary>
  /// Access to the 'Air Pollution API'
  /// </summary>
  /// <remarks>
  /// The 'Air Pollution API' provides current, forecast and historical air pollution data for any coordinates on the globe.
  /// </remarks>
  /// <returns>An implementation of <see cref="IAirPollutionApi"/></returns>
  IAirPollutionApi AirPollution { get; }
}