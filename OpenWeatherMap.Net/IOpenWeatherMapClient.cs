using OpenWeatherMap.Net.Models;

namespace OpenWeatherMap.Net;

public interface IOpenWeatherMapClient
{
  /// <summary>
  /// Query current weather data by location name
  /// </summary>
  /// <remarks>
  /// This function is the same as manually calling <see cref="QueryGeoCodeAsync"/> and then using the coordinates
  /// of the first location in the result set to call <see cref="QueryWeatherAsync(double, double)"/>
  /// </remarks>
  /// <param name="query">City name, state code (only for the US) and country code divided by comma (ISO 3166 country codes)</param>
  /// <returns>An <see cref="IApiResponse{T}"/> containing the current weather data</returns>
  Task<IApiResponse<CurrentWeather>> QueryWeatherAsync(string query);

  /// <summary>
  /// Query current weather data by geographical coordinates (latitude, longitude)
  /// </summary>
  /// <param name="lat">Latitude</param>
  /// <param name="lon">Longitude</param>
  /// <returns>An <see cref="IApiResponse{T}"/> containing the current weather data</returns>
  Task<IApiResponse<CurrentWeather>> QueryWeatherAsync(double lat, double lon);

  /// <summary>
  /// Query geographical coordinates for a location name
  /// </summary>
  /// <param name="query">City name, state code (only for the US) and country code divided by comma (ISO 3166 country codes)</param>
  /// <param name="limit">Number of the locations in the API response (up to 5 results can be returned in the API response)</param>
  /// <returns>An <see cref="IApiResponse{T}"/> containing a list of up to 5 matching locations </returns>
  Task<IApiResponse<IEnumerable<GeoCode>>> QueryGeoCodeAsync(string query, int limit = int.MaxValue);

  /// <summary>
  /// Query for locations (city name or area name) by using geographical coordinates
  /// </summary>
  /// <param name="lat">Latitude</param>
  /// <param name="lon">Longitude</param>
  /// <param name="limit">Number of the location names in the API response (several results can be returned in the API response)</param>
  /// <returns>An <see cref="IApiResponse{T}"/> containing a list of locations </returns>
  Task<IApiResponse<IEnumerable<GeoCode>>> QueryGeoCodeReverseAsync(double lat, double lon, int limit = int.MaxValue);
}