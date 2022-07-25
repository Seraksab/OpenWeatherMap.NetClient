using OpenWeatherMap.NetClient.ApiClients;
using OpenWeatherMap.NetClient.Extensions;
using OpenWeatherMap.NetClient.Models;
using Refit;

namespace OpenWeatherMap.NetClient.Apis.Impl;

internal class CurrentWeatherApiImpl : AbstractApiImplBase, ICurrentWeatherApi
{
  private readonly string _apiKey;

  private readonly ICurrentWeatherApiClient _currentWeatherApiClient;
  private readonly IGeocodingApiClient _geoCodingApiClient;

  public CurrentWeatherApiImpl(string apiKey, IOpenWeatherMapOptions? options) : base(options)
  {
    _apiKey = apiKey;
    _currentWeatherApiClient = RestService.For<ICurrentWeatherApiClient>(BaseUrl);
    _geoCodingApiClient = RestService.For<IGeocodingApiClient>(BaseUrl);
  }

  public async Task<Models.IApiResponse<CurrentWeather>> QueryAsync(string query)
  {
    if (query == null) throw new ArgumentNullException(nameof(query));

    return await CacheRequest($"WeatherByName_{query}", async () =>
    {
      var geoCode = await _geoCodingApiClient.GeoCodeByLocationName(_apiKey, query, 1);
      return geoCode.IsSuccessStatusCode && geoCode.Content != null && geoCode.Content.Any()
        ? await WeatherRequest(geoCode.Content.First().Latitude, geoCode.Content.First().Longitude)
        : new Models.ApiResponse<CurrentWeather>(geoCode.StatusCode, geoCode.ReasonPhrase, null, geoCode.Error);
    });
  }

  public async Task<Models.IApiResponse<CurrentWeather>> QueryAsync(double lat, double lon)
  {
    return await CacheRequest($"WeatherByCoordinates_{lat}_{lon}", () => WeatherRequest(lat, lon));
  }

  private async Task<Models.IApiResponse<CurrentWeather>> WeatherRequest(double lat, double lon)
  {
    var response = await _currentWeatherApiClient.CurrentWeather(_apiKey, Language, lat, lon);
    return new Models.ApiResponse<CurrentWeather>(
      response.StatusCode,
      response.ReasonPhrase,
      response.Content?.ToWeather(),
      response.Error
    );
  }
}