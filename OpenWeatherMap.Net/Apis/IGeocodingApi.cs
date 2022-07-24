using OpenWeatherMap.Net.Models;

namespace OpenWeatherMap.Net.Apis;

public interface IGeocodingApi
{
  /// <summary>
  /// Query geographical coordinates for a location name
  /// </summary>
  /// <param name="query">City name, state code (only for the US) and country code divided by comma (ISO 3166 country codes)</param>
  /// <param name="limit">Number of the locations in the API response (up to 5 results can be returned in the API response)</param>
  /// <returns>An <see cref="IApiResponse{T}"/> containing a list of up to 5 matching locations </returns>
  Task<IApiResponse<IEnumerable<GeoCode>>> QueryAsync(string query, int limit = int.MaxValue);

  /// <summary>
  /// Query for locations (city name or area name) by using geographical coordinates
  /// </summary>
  /// <param name="lat">Latitude</param>
  /// <param name="lon">Longitude</param>
  /// <param name="limit">Number of the location names in the API response (several results can be returned in the API response)</param>
  /// <returns>An <see cref="IApiResponse{T}"/> containing a list of locations </returns>
  Task<IApiResponse<IEnumerable<GeoCode>>> QueryReverseAsync(double lat, double lon, int limit = int.MaxValue);
}