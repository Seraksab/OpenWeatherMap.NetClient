using OpenWeatherMap.NetClient.Extensions;
using OpenWeatherMap.NetClient.Models;
using OpenWeatherMap.NetClient.RestApis.Clients;

namespace OpenWeatherMap.NetClient.Apis;

/// <summary>
/// Implementation of <see cref="ICurrentWeatherApi"/>
/// </summary>
public sealed class CurrentWeatherApi : ICurrentWeatherApi
{
  private const string BaseUrl = "https://api.openweathermap.org";

  private readonly string _apiKey;
  private readonly string _language;

  private readonly RestClient<ICurrentWeatherApiClient> _weatherApi;
  private readonly RestClient<IGeocodingApiClient> _geoApi;

  internal CurrentWeatherApi(string apiKey, OpenWeatherMapOptions options)
  {
    _apiKey = apiKey;
    _language = options.Culture.TwoLetterISOLanguageName;

    _weatherApi = new RestClient<ICurrentWeatherApiClient>(BaseUrl, options);
    _geoApi = new RestClient<IGeocodingApiClient>(BaseUrl, options);
  }

  /// <inheritdoc />
  public async Task<CurrentWeather?> QueryAsync(string query)
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
        var weather = await api.CurrentWeather(_apiKey, _language, geoCode.Latitude, geoCode.Longitude);
        return weather.ToWeather();
      },
      () => $"CurrentWeatherByName_{query}"
    );
  }

  /// <inheritdoc />
  public async Task<CurrentWeather?> GetByCoordinatesAsync(double lat, double lon)
  {
    return await _weatherApi.Call(async api =>
      {
        var weather = await api.CurrentWeather(_apiKey, _language, lat, lon);
        return weather.ToWeather();
      },
      () => $"CurrentWeatherByCoordinates_{lat}_{lon}"
    );
  }

  /// <inheritdoc />
  public async Task<CurrentWeather?> GetByCityIdAsync(int cityId)
  {
    return await _weatherApi.Call(async api =>
      {
        var weather = await api.CurrentWeather(_apiKey, _language, cityId);
        return weather.ToWeather();
      },
      () => $"CurrentWeatherByCityId_{cityId}"
    );
  }
}