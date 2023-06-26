using OpenWeatherMap.NetClient.Models;

namespace OpenWeatherMap.NetClient.Apis;

/// <summary>
/// Access to the 'Air Pollution' API.
/// Provides current, forecast and historical air pollution data for any coordinates on the globe.
/// </summary>
public interface IAirPollutionApi
{
  /// <summary>
  /// The <see cref="HttpClient"/> being used to perform the HTTP requests
  /// </summary>
  HttpClient Client  { get; }

  /// <summary>
  /// Query current air pollution data by geographical coordinates
  /// </summary>
  /// <param name="lat">Latitude</param>
  /// <param name="lon">Longitude</param>
  /// <returns>The current air pollution data</returns>
  Task<AirPollution?> GetCurrentAsync(double lat, double lon);

  /// <summary>
  /// Query forecast air pollution data by geographical coordinates
  /// </summary>
  /// <param name="lat">Latitude</param>
  /// <param name="lon">Longitude</param>
  /// <returns>The forecast air pollution data</returns>
  Task<IEnumerable<AirPollution>> GetForecastAsync(double lat, double lon);

  /// <summary>
  /// Query historical air pollution data by geographical coordinates for a specific time range
  /// </summary>
  /// <param name="lat">Latitude</param>
  /// <param name="lon">Longitude</param>
  /// <param name="start">Start date</param>
  /// <param name="end">End date</param>
  /// <returns>The historical air pollution data</returns>
  Task<IEnumerable<AirPollution>> GetHistoricalAsync(double lat, double lon, DateTimeOffset start, DateTimeOffset end);
}