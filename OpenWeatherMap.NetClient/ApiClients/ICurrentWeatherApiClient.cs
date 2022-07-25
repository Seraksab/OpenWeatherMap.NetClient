using Refit;
using ApiWeatherResponse = OpenWeatherMap.NetClient.Models.ApiResponses.ApiWeatherResponse;

namespace OpenWeatherMap.NetClient.ApiClients;

[Headers("Accept: application/json")]
internal interface ICurrentWeatherApiClient
{
  /// <summary>
  /// Get the current weather data for geographical coordinates
  /// </summary>
  /// <param name="appid">API key</param>
  /// <param name="lang">Language to get textual output in (en, de, ...)</param>
  /// <param name="lat">Geographical coordinates (latitude)</param>
  /// <param name="lon">Geographical coordinates (longitude)</param>
  [Get("/data/2.5/weather")]
  Task<ApiResponse<ApiWeatherResponse>> CurrentWeather(string appid, string lang, double lat, double lon);
}