using OpenWeatherMap.NetClient.RestApis.Responses;
using Refit;

namespace OpenWeatherMap.NetClient.RestApis.Clients;

[Headers("Accept: application/json")]
internal interface IOneCallApiClient
{
  /// <summary>
  /// Get the current weather
  /// </summary>
  /// <param name="appid">API key</param>
  /// <param name="lat">Geographical coordinates (latitude)</param>
  /// <param name="lon">Geographical coordinates (longitude)</param>
  /// <param name="lang">Language to get textual output in (en, de, ...)</param>
  /// <param name="exclude">Exclude parts of the response</param>
  [Get("/data/3.0/onecall")]
  Task<ApiOneCallCurrentResponse> CurrentAndForecast(string appid, double lat, double lon, string lang, string exclude);

  /// <summary>
  /// Historical weather data for any timestamp from 1st January 1979 till now
  /// </summary>
  /// <param name="appid">API key</param>
  /// <param name="lat">Geographical coordinates (latitude)</param>
  /// <param name="lon">Geographical coordinates (longitude)</param>
  /// <param name="lang">Language to get textual output in (en, de, ...)</param>
  /// <param name="dt">Timestamp (Unix time, UTC time zone)</param>
  [Get("/data/3.0/onecall/timemachine")]
  Task<ApiOneCallHistoricalResponse> Historical(string appid, double lat, double lon, string lang, long dt);

  /// <summary>
  /// Aggregated historical weather data for more that 40 years back
  /// </summary>
  /// <param name="appid">API key</param>
  /// <param name="lat">Geographical coordinates (latitude)</param>
  /// <param name="lon">Geographical coordinates (longitude)</param>
  /// <param name="lang">Language to get textual output in (en, de, ...)</param>
  /// <param name="date">Date in the `YYYY-MM-DD` format for which data is requested</param>
  [Get("/data/3.0/onecall/day_summary")]
  Task<ApiOneCallHistoricalDayResponse> HistoricalDay(string appid, double lat, double lon, string lang, string date);
}