using Refit;

namespace OpenWeatherMap.NetClient.RestApis.Clients;

[Headers("Accept: image/png")]
internal interface IWeatherMapsApiClient
{
  /// <summary>
  /// Get the current weather map
  /// </summary>
  /// <param name="appid">API key</param>
  /// <param name="layer">The layer</param>
  /// <param name="zoom">Zoom level</param>
  /// <param name="x">X tile coordinate</param>
  /// <param name="y">Y tile coordinate</param>
  [Get("/map/{layer}/{zoom}/{x}/{y}.png?appid={appid}")]
  Task<HttpResponseMessage> GetWeatherMap(string appid, string layer, int zoom, int x, int y);
}