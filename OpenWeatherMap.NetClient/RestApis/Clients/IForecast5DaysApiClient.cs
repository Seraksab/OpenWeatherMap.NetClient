using OpenWeatherMap.NetClient.RestApis.Responses;
using Refit;

namespace OpenWeatherMap.NetClient.RestApis.Clients;

[Headers("Accept: application/json")]
internal interface IForecast5DaysApiClient
{
  /// <summary>
  /// Get the forecast for geographical coordinates
  /// </summary>
  /// <param name="appid">API key</param>
  /// <param name="lang">Language to get textual output in (en, de, ...)</param>
  /// <param name="lat">Geographical coordinates (latitude)</param>
  /// <param name="lon">Geographical coordinates (longitude)</param>
  /// <param name="cnt">Number of timestamps which will be returned</param>
  [Get("/data/2.5/forecast")]
  Task<ApiResponse<ApiForecast5DaysResponse>> Forecast(string appid, string lang, double lat, double lon, int cnt);
  
  /// <summary>
  /// Get the forecast by city ID
  /// </summary>
  /// <param name="appid">API key</param>
  /// <param name="lang">Language to get textual output in (en, de, ...)</param>
  /// <param name="id">City ID</param>
  /// <param name="cnt">Number of timestamps which will be returned</param>
  [Get("/data/2.5/forecast")]
  Task<ApiResponse<ApiForecast5DaysResponse>> Forecast(string appid, string lang, int id, int cnt);
}