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
      var geoCode = MapGeoCodes(await _apiClient.GeoCodeByLocationName(_apiKey, query, 1));
      return geoCode.IsSuccess && geoCode.Content != null && geoCode.Content.Any()
        ? await WeatherRequest(geoCode.Content.First().Latitude, geoCode.Content.First().Longitude)
        : new Models.ApiResponse<CurrentWeather>(geoCode.StatusCode, geoCode.ReasonPhrase, null, geoCode.Error);
    });
  }

  public async Task<Models.IApiResponse<CurrentWeather>> QueryWeatherAsync(double lat, double lon)
  {
    return await CacheRequest($"WeatherByCoordinates_{lat}_{lon}", () => WeatherRequest(lat, lon));
  }

  public async Task<Models.IApiResponse<IEnumerable<GeoCode>>> QueryGeoCodeAsync(string query, int limit = int.MaxValue)
  {
    return await CacheRequest($"GeoCode_{query}_{limit}", async () =>
      MapGeoCodes(await _apiClient.GeoCodeByLocationName(_apiKey, query, limit))
    );
  }

  public async Task<Models.IApiResponse<IEnumerable<GeoCode>>> QueryGeoCodeReverseAsync(double lat, double lon,
    int limit = int.MaxValue)
  {
    return await CacheRequest($"GeoCodeReverse_{lat}_{lon}_{limit}",
      async () => MapGeoCodes(await _apiClient.GeoCodeReverse(_apiKey, lat, lon, limit))
    );
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

  private async Task<Models.IApiResponse<T>> CacheRequest<T>(string cacheKey,
    Func<Task<Models.IApiResponse<T>>> itemFactory)
    where T : class
  {
    return _cache == null
      ? await itemFactory()
      : await _cache.GetOrAddAsync(cacheKey, itemFactory, _cacheDuration);
  }

  private static Models.IApiResponse<IEnumerable<GeoCode>> MapGeoCodes(
    Refit.IApiResponse<ApiGeoCodeResponse[]> response)
  {
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
}