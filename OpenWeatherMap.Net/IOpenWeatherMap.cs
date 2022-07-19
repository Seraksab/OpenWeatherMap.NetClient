using OpenWeatherMap.Net.Models;

namespace OpenWeatherMap.Net;

public interface IOpenWeatherMap
{
  /// <summary>
  /// Query current weather data by city name
  /// </summary>
  /// <param name="name">City name, state code (only for the US) and country code divided by comma (ISO 3166 country codes)</param>
  Task<ApiWeatherResponse?> CurrentWeatherByName(string name);

  /// <summary>
  /// Query current weather data by zip/post code
  /// </summary>
  /// <param name="zip">Zip/post code and country code divided by comma (ISO 3166 country codes)</param>
  Task<ApiWeatherResponse?> CurrentWeatherByZip(string zip);

  /// <summary>
  /// Query current weather data by graphical coordinates (latitude, longitude)
  /// </summary>
  /// <param name="lat">Latitude</param>
  /// <param name="lon">Longitude</param>
  Task<ApiWeatherResponse?> CurrentWeatherByCoordinates(double lat, double lon);
}