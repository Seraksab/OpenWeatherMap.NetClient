using OpenWeatherMap.NetClient.Models;

namespace OpenWeatherMap.NetClient.Apis;

/// <summary>
/// Access to the 'Current Weather Data API'
/// </summary>
/// <remarks>
/// The 'Current Weather Data API' provides current weather data for any location on Earth including over 200,000 cities.
/// Data is collected and processed from different sources such as global and local weather models, satellites, radars and
/// a vast network of weather stations.
/// </remarks>
public interface ICurrentWeatherApi
{
  /// <summary>
  /// Query current weather data by location name
  /// </summary>
  /// <remarks>
  /// This function is the same as manually calling <see cref="IGeocodingApi.QueryAsync"/> and then using the coordinates
  /// of the first location in the result set to call <see cref="QueryAsync(double,double)"/>
  /// </remarks>
  /// <param name="query">City name, state code (only for the US) and country code divided by comma (ISO 3166 country codes)</param>
  /// <returns>The current weather data</returns>
  Task<CurrentWeather?> QueryAsync(string query);

  /// <summary>
  /// Query current weather data by geographical coordinates (latitude, longitude)
  /// </summary>
  /// <param name="lat">Latitude</param>
  /// <param name="lon">Longitude</param>
  /// <returns>The current weather data</returns>
  Task<CurrentWeather?> QueryAsync(double lat, double lon);

  /// <summary>
  /// Get the current weather data by city ID
  /// </summary>
  /// <param name="cityId">City ID</param>
  /// <returns>The current weather data</returns>
  Task<CurrentWeather?> QueryAsync(int cityId);
}