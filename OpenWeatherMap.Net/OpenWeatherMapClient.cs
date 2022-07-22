using System.Text.RegularExpressions;
using LazyCache;
using OpenWeatherMap.Net.Api;
using OpenWeatherMap.Net.Extensions;
using OpenWeatherMap.Net.Models;
using Refit;

namespace OpenWeatherMap.Net;

public class OpenWeatherMapClient : IOpenWeatherMapClient
{
  private static readonly Regex ApiKeyRegex = new(@"^[0-9a-f]{32}$");

  private const string BaseUrl = "https://api.openweathermap.org";

  private readonly string _apiKey;
  private readonly string _lang;
  private readonly TimeSpan _cacheDuration;
  private readonly IOpenWeatherMapApi _apiClient;
  private readonly IAppCache _cache;

  public OpenWeatherMapClient(string apiKey, OpenWeatherMapOptions? options = null)
  {
    if (apiKey == null) throw new ArgumentNullException(nameof(apiKey));
    if (!ApiKeyRegex.IsMatch(apiKey)) throw new ArgumentException($"'{apiKey}' is not a valid API key");

    _apiKey = apiKey;
    _lang = (options?.Culture ?? OpenWeatherMapOptions.DefaultCulture).TwoLetterISOLanguageName;
    _cacheDuration = options?.CacheDuration ?? OpenWeatherMapOptions.DefaultCacheDuration;

    _apiClient = RestService.For<IOpenWeatherMapApi>(BaseUrl);
    _cache = new CachingService();
  }

  public async Task<IResponse<CurrentWeather>> CurrentWeatherByName(string name)
  {
    if (name == null) throw new ArgumentNullException(nameof(name));

    return await _cache.GetOrAddAsync(
      $"ByName_{name}", async () =>
      {
        var result = await _apiClient.CoordinatesByLocationName(_apiKey, 1, name);
        return result.IsSuccessStatusCode && result.Content is { Length: > 0 }
          ? await WeatherRequest(result.Content[0].Latitude, result.Content[0].Longitude)
          : new Response<CurrentWeather>(result.StatusCode, result.ReasonPhrase, null, result.Error);
      },
      _cacheDuration
    );
  }

  public async Task<IResponse<CurrentWeather>> CurrentWeatherByZip(string zip)
  {
    if (zip == null) throw new ArgumentNullException(nameof(zip));

    return await _cache.GetOrAddAsync(
      $"ByZip_{zip}", async () =>
      {
        var result = await _apiClient.CoordinatesByZipCode(_apiKey, zip);
        return result.IsSuccessStatusCode && result.Content != null
          ? await WeatherRequest(result.Content.Latitude, result.Content.Longitude)
          : new Response<CurrentWeather>(result.StatusCode, result.ReasonPhrase, null, result.Error);
      },
      _cacheDuration
    );
  }

  public async Task<IResponse<CurrentWeather>> CurrentWeatherByCoordinates(double lat, double lon)
  {
    return await _cache.GetOrAddAsync(
      $"ByCoordinates_{lat}_{lon}",
      () => WeatherRequest(lat, lon),
      _cacheDuration
    );
  }

  private async Task<IResponse<CurrentWeather>> WeatherRequest(double lat, double lon)
  {
    var response = await _apiClient.CurrentWeather(_apiKey, _lang, lat, lon);
    return new Response<CurrentWeather>(response.StatusCode, response.ReasonPhrase, response.Content?.ToWeather(), response.Error);
  }
}