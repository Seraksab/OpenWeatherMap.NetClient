using Refit;
using ApiGeoCodeResponse = OpenWeatherMap.Net.Models.ApiGeoCodeResponse;
using ApiWeatherResponse = OpenWeatherMap.Net.Models.ApiWeatherResponse;

namespace OpenWeatherMap.Net.Api;

[Headers("Accept: application/json")]
internal interface IOpenWeatherMapApi
{
  /// <summary>
  /// Current weather data
  /// </summary>
  /// <param name="appid">API key</param>
  /// <param name="lang">Language to get textual output in (en, de, ...)</param>
  /// <param name="lat">Geographical coordinates (latitude)</param>
  /// <param name="lon">Geographical coordinates (longitude)</param>
  [Get("/data/2.5/weather")]
  Task<ApiResponse<ApiWeatherResponse>> CurrentWeather(string appid, string lang, double lat, double lon);

  /// <summary>
  /// Coordinates by location name
  /// </summary>
  /// <param name="appid">API key</param>
  /// <param name="limit">Number of the locations in the API response (up to 5)</param>
  /// <param name="q">City name, state code (only for the US) and country code divided by comma (ISO 3166 country codes)</param>
  [Get("/geo/1.0/direct")]
  Task<ApiResponse<ApiGeoCodeResponse[]>> GeoCodeByLocationName(string appid, int limit, string q);

  /// <summary>
  /// Coordinates by zip/post code
  /// </summary>
  /// <param name="appid">API key</param>
  /// <param name="zip">Zip/post code and country code divided by comma (ISO 3166 country codes)</param>
  [Get("/geo/1.0/zip")]
  Task<ApiResponse<ApiGeoCodeResponse?>> GeoCodeByZipCode(string appid, string zip);
}