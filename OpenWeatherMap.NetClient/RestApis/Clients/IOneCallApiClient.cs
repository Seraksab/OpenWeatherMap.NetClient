﻿using OpenWeatherMap.NetClient.RestApis.Responses;
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
}