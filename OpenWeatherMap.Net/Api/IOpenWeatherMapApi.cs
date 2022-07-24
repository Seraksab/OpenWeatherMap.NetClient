using Refit;
using ApiGeoCodeResponse = OpenWeatherMap.Net.Models.ApiGeoCodeResponse;
using ApiWeatherResponse = OpenWeatherMap.Net.Models.ApiWeatherResponse;

namespace OpenWeatherMap.Net.Api;

[Headers("Accept: application/json")]
internal interface IOpenWeatherMapApi
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

  /// <summary>
  /// Get geographical coordinates for a location name
  /// </summary>
  /// <param name="appid">API key</param>
  /// <param name="q">City name, state code (only for the US) and country code divided by comma (ISO 3166 country codes)</param>
  /// <param name="limit">Number of the locations in the API response (up to 5)</param>
  [Get("/geo/1.0/direct")]
  Task<ApiResponse<ApiGeoCodeResponse[]>> GeoCodeByLocationName(string appid, string q, int limit);

  /// <summary>
  /// Get geographical coordinates for a zip/post code
  /// </summary>
  /// <param name="appid">API key</param>
  /// <param name="zip">Zip/post code and country code divided by comma (ISO 3166 country codes)</param>
  [Get("/geo/1.0/zip")]
  Task<ApiResponse<ApiGeoCodeResponse?>> GeoCodeByZipCode(string appid, string zip);

  /// <summary>
  /// Get the name of a location by using geographical coordinates
  /// </summary>
  /// <param name="appid">API key</param>
  /// <param name="lat">Geographical coordinates (latitude)</param>
  /// <param name="lon">Geographical coordinates (longitude)</param>
  /// <param name="limit">Number of the location names in the API response (several results can be returned in the API response)</param>
  [Get("/geo/1.0/reverse")]
  Task<ApiResponse<ApiGeoCodeResponse[]>> GeoCodeReverse(string appid, double lat, double lon, int limit);
}