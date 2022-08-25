using OpenWeatherMap.NetClient.Models;

namespace OpenWeatherMap.NetClient.Apis;

/// <summary>
/// Access to the '3-hour Forecast 5 days' API.
/// The 5 day forecast includes weather data with 3-hour steps.
/// </summary>
public interface IForecast5DaysApi
{
  /// <summary>
  /// Query the weather forecast data by location name
  /// </summary>
  /// <remarks>
  /// This function is the same as manually calling <see cref="IGeocodingApi.QueryAsync"/> and then using the coordinates
  /// of the first location in the result set to call <see cref="QueryByCoordinatesAsync(double,double,int)"/>
  /// </remarks>
  /// <param name="query">City name, state code (only for the US) and country code divided by comma (ISO 3166 country codes)</param>
  /// <param name="limit">Number of timestamps that will be returned</param>
  /// <returns>The current weather data</returns>
  Task<Forecast5Days?> QueryAsync(string query, int limit = int.MaxValue);

  /// <summary>
  /// Query the weather forecast data by geographical coordinates (latitude, longitude)
  /// </summary>
  /// <param name="lat">Latitude</param>
  /// <param name="lon">Longitude</param>
  /// <param name="limit">Number of timestamps that will be returned</param>
  /// <returns>The current weather data</returns>
  Task<Forecast5Days?> QueryByCoordinatesAsync(double lat, double lon, int limit = int.MaxValue);

  /// <summary>
  /// Query the weather forecast data by city ID
  /// </summary>
  /// <param name="cityId">City ID</param>
  /// <param name="limit">Number of timestamps that will be returned</param>
  /// <returns>The current weather data</returns>
  Task<Forecast5Days?> QueryByCityIdAsync(int cityId, int limit = int.MaxValue);
}