using Refit;

namespace OpenWeatherMap.NetClient.RestApis.Clients;

[Headers("Accept: image/png")]
internal interface IWeatherMapsApiClient
{
  [Get("/map/{layer}/{zoom}/{x}/{y}.png?appid={appid}")]
  Task<HttpResponseMessage> GetWeatherMap(string appid, string layer, int zoom, int x, int y);
}