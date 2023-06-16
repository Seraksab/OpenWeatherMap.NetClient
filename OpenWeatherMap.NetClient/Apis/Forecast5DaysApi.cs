using OpenWeatherMap.NetClient.Extensions;
using OpenWeatherMap.NetClient.Models;
using OpenWeatherMap.NetClient.RestApis.Clients;

namespace OpenWeatherMap.NetClient.Apis;

/// <summary>
/// Implementation of <see cref="IForecast5DaysApi"/>
/// </summary>
public sealed class Forecast5DaysApi : IForecast5DaysApi
{
  private const string BaseUrl = "https://api.openweathermap.org";

  private readonly string _apiKey;
  private readonly string _language;

  private readonly HttpClient _httpClient;
  private readonly RestClient<IForecast5DaysApiClient> _forecastApi;
  private readonly RestClient<IGeocodingApiClient> _geoApi;

  internal Forecast5DaysApi(string apiKey, OpenWeatherMapOptions options)
  {
    _apiKey = apiKey;
    _language = options.Culture.TwoLetterISOLanguageName;

    _httpClient = new HttpClient
    {
      BaseAddress = new Uri(BaseUrl)
    };
    _forecastApi = new RestClient<IForecast5DaysApiClient>(_httpClient, options);
    _geoApi = new RestClient<IGeocodingApiClient>(_httpClient, options);
  }

  /// <inheritdoc />
  public HttpClient Client => _httpClient;

  /// <inheritdoc />
  public async Task<Forecast5Days?> QueryAsync(string query, int limit = int.MaxValue)
  {
    if (query == null) throw new ArgumentNullException(nameof(query));

    var response = await _geoApi.Call(
      api => api.GeoCodeByLocationName(_apiKey, query, 1),
      () => $"GeoCodeByLocationName_{query}"
    );
    if (!response.Any()) return null;

    var geoCode = response.First();
    return await _forecastApi.Call(async api =>
      {
        var forecast = await api.Forecast(_apiKey, _language, geoCode.Latitude, geoCode.Longitude, limit);
        return forecast.ToForecast();
      },
      () => $"Forecast5DaysByName_{query}"
    );
  }

  /// <inheritdoc />
  public async Task<Forecast5Days?> GetByCoordinatesAsync(double lat, double lon, int limit = int.MaxValue)
  {
    return await _forecastApi.Call(async api =>
      {
        var forecast = await api.Forecast(_apiKey, _language, lat, lon, limit);
        return forecast.ToForecast();
      },
      () => $"Forecast5DaysByCoordinates_{lat}_{lon}"
    );
  }

  /// <inheritdoc />
  public async Task<Forecast5Days?> GetByCityIdAsync(int cityId, int limit = int.MaxValue)
  {
    return await _forecastApi.Call(async api =>
      {
        var forecast = await api.Forecast(_apiKey, _language, cityId, limit);
        return forecast.ToForecast();
      },
      () => $"Forecast5DaysByCityId_{cityId}"
    );
  }
}