using System.Text.RegularExpressions;
using LazyCache;
using OpenWeatherMap.Net.Api;
using OpenWeatherMap.Net.Extensions;
using OpenWeatherMap.Net.Models;
using Refit;

namespace OpenWeatherMap.Net;

public class OpenWeatherMap : IOpenWeatherMap
{
  private static readonly Regex ApiKeyRegex = new(@"^[0-9a-f]{32}$");

  private const string BaseUrl = "https://api.openweathermap.org";

  private readonly string _apiKey;
  private readonly string _lang;
  private readonly TimeSpan _cacheDuration;
  private readonly IOpenWeatherMapApi _apiClient;
  private readonly IAppCache _cache;

  public OpenWeatherMap(string apiKey, OpenWeatherMapOptions? options = null)
  {
    if (apiKey == null) throw new ArgumentNullException(nameof(apiKey));
    if (!ApiKeyRegex.IsMatch(apiKey)) throw new ArgumentException($"'{apiKey}' is not a valid API key");

    _apiKey = apiKey;
    _lang = (options?.Culture ?? OpenWeatherMapOptions.DefaultCulture).TwoLetterISOLanguageName;
    _cacheDuration = options?.CacheDuration ?? OpenWeatherMapOptions.DefaultCacheDuration;

    _apiClient = RestService.For<IOpenWeatherMapApi>(BaseUrl);
    _cache = new CachingService();
  }

  public async Task<CurrentWeather?> CurrentWeatherByName(string name)
  {
    if (name == null) throw new ArgumentNullException(nameof(name));

    return await _cache.GetOrAddAsync(
      $"ByName_{name}", async () =>
      {
        var result = await Request(() => _apiClient.CoordinatesByLocationName(_apiKey, 1, name));
        if (result == null || result.Length == 0) return null;
        return await WeatherRequest(result[0].Latitude, result[0].Longitude);
      },
      _cacheDuration
    );
  }

  public async Task<CurrentWeather?> CurrentWeatherByZip(string zip)
  {
    if (zip == null) throw new ArgumentNullException(nameof(zip));

    return await _cache.GetOrAddAsync(
      $"ByZip_{zip}", async () =>
      {
        var result = await Request(() => _apiClient.CoordinatesByZipCode(_apiKey, zip));
        if (result == null) return null;
        return await WeatherRequest(result.Latitude, result.Longitude);
      },
      _cacheDuration
    );
  }

  public async Task<CurrentWeather?> CurrentWeatherByCoordinates(double lat, double lon)
  {
    return await _cache.GetOrAddAsync(
      $"ByCoordinates_{lat}_{lon}",
      () => WeatherRequest(lat, lon),
      _cacheDuration
    );
  }

  private async Task<CurrentWeather?> WeatherRequest(double lat, double lon)
  {
    var result = await Request(() => _apiClient.CurrentWeather(_apiKey, _lang, lat, lon));
    return result?.ToWeather();
  }

  private static async Task<T?> Request<T>(Func<Task<ApiResponse<T>>> apiFunc)
  {
    var response = await apiFunc();
    if (!response.IsSuccessStatusCode || response.Error != null)
    {
      throw new OpenWeatherMapException(response);
    }

    return response.Content;
  }
}