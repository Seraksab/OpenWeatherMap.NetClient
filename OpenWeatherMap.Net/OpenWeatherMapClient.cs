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
  private readonly IAppCache? _cache;

  public OpenWeatherMapClient(string apiKey, IOpenWeatherMapOptions? options = null)
  {
    if (apiKey == null) throw new ArgumentNullException(nameof(apiKey));
    if (!ApiKeyRegex.IsMatch(apiKey)) throw new ArgumentException($"'{apiKey}' is not a valid API key");

    _apiKey = apiKey;
    _lang = (options?.Culture ?? OpenWeatherMapOptions.Defaults.Culture).TwoLetterISOLanguageName;
    _cacheDuration = options?.CacheDuration ?? OpenWeatherMapOptions.Defaults.CacheDuration;

    _apiClient = RestService.For<IOpenWeatherMapApi>(BaseUrl);

    if (options?.CacheEnabled ?? OpenWeatherMapOptions.Defaults.CacheEnabled)
    {
      _cache = new CachingService();
    }
  }

  public async Task<Models.IApiResponse<CurrentWeather>> QueryWeatherAsync(string query)
  {
    if (query == null) throw new ArgumentNullException(nameof(query));

    return await CacheRequest($"WeatherByName_{query}", async () =>
    {
      var geoCode = await GeoCodeRequest(query, 1);
      return geoCode.IsSuccess && geoCode.Content != null && geoCode.Content.Any()
        ? await WeatherRequest(geoCode.Content.First().Latitude, geoCode.Content.First().Longitude)
        : new Models.ApiResponse<CurrentWeather>(geoCode.StatusCode, geoCode.ReasonPhrase, null, geoCode.Error);
    });
  }

  public async Task<Models.IApiResponse<CurrentWeather>> QueryWeatherAsync(double lat, double lon)
  {
    return await CacheRequest($"WeatherByCoordinates_{lat}_{lon}", () => WeatherRequest(lat, lon));
  }

  public async Task<Models.IApiResponse<IEnumerable<GeoCode>>> QueryGeoCodeAsync(string query)
  {
    return await CacheRequest($"GeoCode_{query}", () => GeoCodeRequest(query, 5));
  }

  private async Task<Models.IApiResponse<CurrentWeather>> WeatherRequest(double lat, double lon)
  {
    var response = await _apiClient.CurrentWeather(_apiKey, _lang, lat, lon);
    return new Models.ApiResponse<CurrentWeather>(
      response.StatusCode,
      response.ReasonPhrase,
      response.Content?.ToWeather(),
      response.Error
    );
  }

  private async Task<Models.IApiResponse<IEnumerable<GeoCode>>> GeoCodeRequest(string query, int limit)
  {
    var response = await _apiClient.GeoCodeByLocationName(_apiKey, limit, query);
    var mappedResponse = response.Content == null
      ? Enumerable.Empty<GeoCode>()
      : response.Content.Select(gc => gc.ToGeoCode());
    return new Models.ApiResponse<IEnumerable<GeoCode>>(
      response.StatusCode,
      response.ReasonPhrase,
      mappedResponse,
      response.Error
    );
  }

  private async Task<Models.IApiResponse<T>> CacheRequest<T>(string cacheKey,
    Func<Task<Models.IApiResponse<T>>> itemFactory)
    where T : class
  {
    return _cache == null
      ? await itemFactory()
      : await _cache.GetOrAddAsync(cacheKey, itemFactory, _cacheDuration);
  }
}