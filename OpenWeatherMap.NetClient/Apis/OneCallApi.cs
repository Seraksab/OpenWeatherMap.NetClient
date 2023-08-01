using OpenWeatherMap.NetClient.Enums;
using OpenWeatherMap.NetClient.Extensions;
using OpenWeatherMap.NetClient.Models;
using OpenWeatherMap.NetClient.RestApis.Clients;
using OpenWeatherMap.NetClient.RestApis.Responses;

namespace OpenWeatherMap.NetClient.Apis;

/// <summary>
/// Implementation of <see cref="IOneCallApi"/>
/// </summary>
public sealed class OneCallApi : IOneCallApi
{
  private const string BaseUrl = "https://api.openweathermap.org";

  private readonly string _apiKey;
  private readonly string _language;

  private readonly HttpClient _httpClient;
  private readonly RestClient<IOneCallApiClient> _weatherApi;
  private readonly RestClient<IGeocodingApiClient> _geoApi;

  internal OneCallApi(string apiKey, OpenWeatherMapOptions options)
  {
    _apiKey = apiKey;
    _language = options.Culture.TwoLetterISOLanguageName;

    _httpClient = new HttpClient
    {
      BaseAddress = new Uri(BaseUrl)
    };
    _weatherApi = new RestClient<IOneCallApiClient>(_httpClient, options);
    _geoApi = new RestClient<IGeocodingApiClient>(_httpClient, options);
  }

  /// <inheritdoc />
  public HttpClient Client => _httpClient;

  /// <inheritdoc />
  public async Task<OneCallCurrentWeather?> QueryAsync(string query, IEnumerable<OneCallCategory>? exclude = null)
  {
    if (query == null) throw new ArgumentNullException(nameof(query));

    var geoResponse = await GeoQuery(query);
    if (!geoResponse.Any()) return null;

    var excludeString = ExcludeString(exclude);
    var geoCode = geoResponse.First();
    return await _weatherApi.Call(async api =>
      {
        var weather = await api.CurrentAndForecast(
          _apiKey, geoCode.Latitude, geoCode.Longitude, _language, excludeString
        );
        return weather.ToWeather();
      },
      () => $"CurrentWeatherByName_{query}_{excludeString}"
    );
  }

  /// <inheritdoc />
  public async Task<OneCallCurrentWeather?> GetByCoordinatesAsync(
    double lat, double lon, IEnumerable<OneCallCategory>? exclude = null
  )
  {
    var excludeString = ExcludeString(exclude);
    return await _weatherApi.Call(async api =>
      {
        var weather = await api.CurrentAndForecast(_apiKey, lat, lon, _language, excludeString);
        return weather.ToWeather();
      },
      () => $"CurrentWeatherByCoordinates_{lat}_{lon}_{excludeString}"
    );
  }

  /// <inheritdoc />
  public async Task<OneCallHistoricalWeather?> QueryHistoricalAsync(string query, DateTimeOffset date)
  {
    if (query == null) throw new ArgumentNullException(nameof(query));

    var geoResponse = await GeoQuery(query);
    if (!geoResponse.Any()) return null;

    var geoCode = geoResponse.First();
    return await _weatherApi.Call(async api =>
      {
        var weather = await api.Historical(
          _apiKey, geoCode.Latitude, geoCode.Longitude, _language, date.ToUnixTimeSeconds()
        );
        return weather.ToWeather();
      },
      () => $"CurrentWeatherByName_{query}_{date.ToUnixTimeSeconds()}"
    );
  }

  /// <inheritdoc />
  public async Task<OneCallHistoricalWeather?> GetHistoricalByCoordinatesAsync(double lat, double lon,
    DateTimeOffset date)
  {
    return await _weatherApi.Call(async api =>
      {
        var weather = await api.Historical(_apiKey, lat, lon, _language, date.ToUnixTimeSeconds());
        return weather.ToWeather();
      },
      () => $"HistoricalWeatherByCoordinates_{lat}_{lon}_{date.ToUnixTimeSeconds()}"
    );
  }

  private Task<ApiGeoCodeResponse[]> GeoQuery(string query)
  {
    return _geoApi.Call(
      api => api.GeoCodeByLocationName(_apiKey, query, 1),
      () => $"GeoCodeByLocationName_{query}"
    );
  }

  private static string ExcludeString(IEnumerable<OneCallCategory>? exclude)
  {
    return exclude is null
      ? string.Empty
      : string.Join(",", exclude.Select(occ => occ.ToString().ToLower()));
  }
}