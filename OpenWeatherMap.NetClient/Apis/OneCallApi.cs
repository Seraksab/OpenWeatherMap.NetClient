using OpenWeatherMap.NetClient.Enums;
using OpenWeatherMap.NetClient.Extensions;
using OpenWeatherMap.NetClient.Models;
using OpenWeatherMap.NetClient.RestApis.Clients;

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
  public async Task<CurrentWeatherOneCall?> QueryAsync(string query, IEnumerable<OneCallCategory>? exclude = null)
  {
    if (query == null) throw new ArgumentNullException(nameof(query));

    var response = await _geoApi.Call(
      api => api.GeoCodeByLocationName(_apiKey, query, 1),
      () => $"GeoCodeByLocationName_{query}"
    );
    if (!response.Any()) return null;

    var geoCode = response.First();
    return await _weatherApi.Call(async api =>
      {
        var weather = await api.CurrentAndForecast(
          _apiKey, geoCode.Latitude, geoCode.Longitude, _language, ExcludeString(exclude)
        );
        return weather.ToWeather();
      },
      () => $"CurrentWeatherByName_{query}"
    );
  }

  /// <inheritdoc />
  public async Task<CurrentWeatherOneCall?> GetByCoordinatesAsync(
    double lat, double lon, IEnumerable<OneCallCategory>? exclude = null
  )
  {
    return await _weatherApi.Call(async api =>
      {
        var weather = await api.CurrentAndForecast(_apiKey, lat, lon, _language, ExcludeString(exclude));
        return weather.ToWeather();
      },
      () => $"CurrentWeatherByCoordinates_{lat}_{lon}"
    );
  }

  private static string ExcludeString(IEnumerable<OneCallCategory>? exclude)
  {
    return exclude is null 
      ? string.Empty 
      : string.Join(",", exclude.Select(occ => occ.ToString().ToLower()));
  }
}