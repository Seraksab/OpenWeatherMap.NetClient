namespace OpenWeatherMap.NetClient.Apis;

/// <summary>
/// Access to 'Weather maps 1.0' API.
/// Provides many kinds of weather maps including Precipitation, Clouds, Pressure, Temperature, Wind.
/// </summary>
/// <remarks>
/// Caching NOT supported
/// </remarks>
public interface IWeatherMapsApi
{
  /// <summary>
  /// Get the current weather map as .png
  /// </summary>
  /// <remarks>
  /// Caching NOT supported
  /// </remarks>
  /// <param name="layer">Layer name</param>
  /// <param name="zoom">Zoom level</param>
  /// <param name="x">X tile coordinate</param>
  /// <param name="y">Y tile coordinate</param>
  /// <returns>The current weather map as byte[]</returns>
  Task<byte[]> GetMapAsync(string layer, int zoom, int x, int y);
}