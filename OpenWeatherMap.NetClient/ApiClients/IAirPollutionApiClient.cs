using OpenWeatherMap.NetClient.Models.ApiResponses;
using Refit;

namespace OpenWeatherMap.NetClient.ApiClients;

[Headers("Accept: application/json")]
internal interface IAirPollutionApiClient

{
  /// <summary>
  /// Get the current air pollution data
  /// </summary>
  /// <param name="appid">API key</param>
  /// <param name="lat">Geographical coordinates (latitude)</param>
  /// <param name="lon">Geographical coordinates (longitude)</param>
  [Get("/data/2.5/air_pollution")]
  Task<ApiResponse<ApiAirPollutionResponse>> Current(string appid, double lat, double lon);

  /// <summary>
  /// Get the forecast air pollution data
  /// </summary>
  /// <param name="appid">API key</param>
  /// <param name="lat">Geographical coordinates (latitude)</param>
  /// <param name="lon">Geographical coordinates (longitude)</param>
  [Get("/data/2.5/air_pollution/forecast")]
  Task<ApiResponse<ApiAirPollutionResponse>> Forecast(string appid, double lat, double lon);

  /// <summary>
  /// Get the historical air pollution data
  /// </summary>
  /// <param name="appid">API key</param>
  /// <param name="lat">Geographical coordinates (latitude)</param>
  /// <param name="lon">Geographical coordinates (longitude)</param>
  /// <param name="start">Start date (unix time, UTC time zone), e.g. start=1606488670</param>
  /// <param name="end">End date (unix time, UTC time zone), e.g. end=1606747870</param>
  [Get("/data/2.5/air_pollution/history")]
  Task<ApiResponse<ApiAirPollutionResponse>> Historical(string appid, double lat, double lon, long start, long end);
}