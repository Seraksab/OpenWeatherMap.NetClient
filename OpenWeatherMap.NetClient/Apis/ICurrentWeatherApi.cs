using OpenWeatherMap.NetClient.Models;

namespace OpenWeatherMap.NetClient.Apis;

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
  /// <returns>An <see cref="IApiResponse{T}"/> containing the current weather data</returns>
  Task<IApiResponse<CurrentWeather>> QueryAsync(string query);

  /// <summary>
  /// Query current weather data by geographical coordinates (latitude, longitude)
  /// </summary>
  /// <param name="lat">Latitude</param>
  /// <param name="lon">Longitude</param>
  /// <returns>An <see cref="IApiResponse{T}"/> containing the current weather data</returns>
  Task<IApiResponse<CurrentWeather>> QueryAsync(double lat, double lon);
}