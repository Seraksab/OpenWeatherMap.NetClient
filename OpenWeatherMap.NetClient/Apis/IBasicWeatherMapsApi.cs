using OpenWeatherMap.NetClient.Models;

namespace OpenWeatherMap.NetClient.Apis;

/// <summary>
/// Access to 'Basic weather maps' API.
/// Provides many kinds of weather maps including Precipitation, Clouds, Pressure, Temperature, Wind.
/// </summary>
/// <remarks>
/// Caching NOT supported
/// </remarks>
public interface IBasicWeatherMapsApi
{
  /// <summary>
  /// The <see cref="HttpClient"/> being used to perform the HTTP requests
  /// </summary>
  HttpClient Client { get; }

  /// <summary>
  /// Get the current weather map as .png
  /// </summary>
  /// <remarks>
  /// Caching NOT supported
  /// </remarks>
  /// <param name="layer">The layer</param>
  /// <param name="zoom">Zoom level</param>
  /// <param name="x">X tile coordinate</param>
  /// <param name="y">Y tile coordinate</param>
  /// <returns>The current weather map as byte[]</returns>
  Task<byte[]> GetMapAsync(BasicWeatherMapLayer layer, int zoom, int x, int y);
}