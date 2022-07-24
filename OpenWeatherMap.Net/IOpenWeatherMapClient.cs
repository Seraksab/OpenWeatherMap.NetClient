using OpenWeatherMap.Net.Apis;

namespace OpenWeatherMap.Net;

public interface IOpenWeatherMapClient
{
  /// <summary>
  /// API to fetch current weather data for any location on Earth including over 200,000 cities. 
  /// </summary>
  /// <returns>An implementation of <see cref="ICurrentWeatherApi"/></returns>
  ICurrentWeatherApi CurrentWeather { get; }

  /// <summary>
  /// The Geocoding API is a tool to ease the search for locations while working with geographic names and coordinates.
  /// </summary>
  /// <returns>An implementation of <see cref="IGeocodingApi"/></returns>
  IGeocodingApi Geocoding { get; }
}