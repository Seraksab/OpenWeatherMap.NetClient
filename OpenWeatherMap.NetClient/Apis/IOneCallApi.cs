using OpenWeatherMap.NetClient.Enums;
using OpenWeatherMap.NetClient.Models;

namespace OpenWeatherMap.NetClient.Apis;

/// <summary>
/// Access to the OneCall 3.0 API.
/// Provides current weather, minute forecast for 1 hour, hourly forecast for 48 hours, daily forecast for 8 days and
/// government weather alerts
/// </summary>
public interface IOneCallApi
{
  /// <summary>
  /// The <see cref="HttpClient"/> being used to perform the HTTP requests
  /// </summary>
  HttpClient Client { get; }

  /// <summary>
  /// Query current weather and forecast data by location name
  /// </summary>
  /// <remarks>
  /// This function is the same as manually calling <see cref="IGeocodingApi.QueryAsync"/> of the <see cref="IGeocodingApi"/>
  /// and then using the coordinates of the first location in the result set to call <see cref="GetByCoordinatesAsync"/>
  /// </remarks>
  /// <param name="query">City name, state code (only for the US) and country code divided by comma (ISO 3166 country codes)</param>
  /// <param name="exclude">Exclude selected parts of the weather data from the API response (optional)</param>
  /// <returns>The current weather data</returns>
  Task<OneCallCurrentWeather?> QueryAsync(string query, IEnumerable<OneCallCategory>? exclude = null);

  /// <summary>
  /// Get current weather data by geographical coordinates (latitude, longitude)
  /// </summary>
  /// <param name="lat">Latitude</param>
  /// <param name="lon">Longitude</param>
  /// <param name="exclude">Exclude selected parts of the weather data from the API response (optional)</param>
  /// <returns>The current weather data</returns>
  Task<OneCallCurrentWeather?> GetByCoordinatesAsync(
    double lat, double lon, IEnumerable<OneCallCategory>? exclude = null
  );

  /// <summary>
  /// Query historical weather data by location name.
  /// Data is available from January 1st, 1979
  /// </summary>
  /// <remarks>
  /// This function is the same as manually calling <see cref="IGeocodingApi.QueryAsync"/> of the <see cref="IGeocodingApi"/>
  /// and then using the coordinates of the first location in the result set to call <see cref="GetHistoricalByCoordinatesAsync"/>
  /// </remarks>
  /// <param name="query">City name, state code (only for the US) and country code divided by comma (ISO 3166 country codes)</param>
  /// <param name="date">The date and time to get weather data for</param>
  /// <returns>The current weather data</returns>
  Task<OneCallHistoricalWeather?> QueryHistoricalAsync(string query, DateTimeOffset date);

  /// <summary>
  /// Get historical weather data by geographical coordinates (latitude, longitude).
  /// Data is available from January 1st, 1979
  /// </summary>
  /// <param name="lat">Latitude</param>
  /// <param name="lon">Longitude</param>
  /// <param name="date">The date and time to get weather data for</param>
  /// <returns>The current weather data</returns>
  Task<OneCallHistoricalWeather?> GetHistoricalByCoordinatesAsync(double lat, double lon, DateTimeOffset date);

  /// <summary>
  /// Query aggregated historical weather data by location name
  /// </summary>
  /// <remarks>
  /// This function is the same as manually calling <see cref="IGeocodingApi.QueryAsync"/> of the <see cref="IGeocodingApi"/>
  /// and then using the coordinates of the first location in the result set to call <see cref="GetHistoricalDayByCoordinatesAsync"/>
  /// </remarks>
  /// <param name="query">City name, state code (only for the US) and country code divided by comma (ISO 3166 country codes)</param>
  /// <param name="date">The date of the day to get weather data for</param>
  /// <returns>The current weather data</returns>
  Task<OneCallHistoricalDayWeather?> QueryHistoricalDayAsync(string query, DateTimeOffset date);

  /// <summary>
  /// Get aggregated historical weather data by geographical coordinates (latitude, longitude).
  /// </summary>
  /// <param name="lat">Latitude</param>
  /// <param name="lon">Longitude</param>
  /// <param name="date">The date of the day to get weather data for</param>
  /// <returns>The current weather data</returns>
  Task<OneCallHistoricalDayWeather?> GetHistoricalDayByCoordinatesAsync(double lat, double lon, DateTimeOffset date);
}