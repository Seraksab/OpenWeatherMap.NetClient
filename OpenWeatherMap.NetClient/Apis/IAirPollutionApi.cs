using OpenWeatherMap.NetClient.Models;

namespace OpenWeatherMap.NetClient.Apis;

/// <summary>
/// Access to the 'Air Pollution API'
/// </summary>
/// <remarks>
/// The 'Air Pollution API' provides current, forecast and historical air pollution data for any coordinates on the globe.
/// </remarks>
public interface IAirPollutionApi
{
  /// <summary>
  /// Query current air pollution data by geographical coordinates
  /// </summary>
  /// <param name="lat">Latitude</param>
  /// <param name="lon">Longitude</param>
  /// <returns>An <see cref="IApiResponse{T}"/> containing the current weather data</returns>
  Task<IApiResponse<AirPollution>> QueryCurrentAsync(double lat, double lon);

  /// <summary>
  /// Query forecast air pollution data by geographical coordinates
  /// </summary>
  /// <param name="lat">Latitude</param>
  /// <param name="lon">Longitude</param>
  /// <returns>An <see cref="IApiResponse{T}"/> containing the current weather data</returns>
  Task<IApiResponse<IEnumerable<AirPollution>>> QueryForecastAsync(double lat, double lon);

  /// <summary>
  /// Query historical air pollution data by geographical coordinates for a specific time range
  /// </summary>
  /// <param name="lat">Latitude</param>
  /// <param name="lon">Longitude</param>
  /// <param name="start">Start date (UTC time zone)</param>
  /// <param name="end">End date (UTC time zone)</param>
  /// <returns>An <see cref="IApiResponse{T}"/> containing the current weather data</returns>
  Task<IApiResponse<IEnumerable<AirPollution>>> QueryHistoricalAsync(double lat, double lon, DateTime start, DateTime end);
}