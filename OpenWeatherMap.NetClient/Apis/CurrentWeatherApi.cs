using OpenWeatherMap.NetClient.Exceptions;
using OpenWeatherMap.NetClient.Extensions;
using OpenWeatherMap.NetClient.Models;
using OpenWeatherMap.NetClient.RestApis.Clients;
using OpenWeatherMap.NetClient.RestApis.Responses;
using Refit;
using ApiException = OpenWeatherMap.NetClient.Exceptions.ApiException;

namespace OpenWeatherMap.NetClient.Apis;

/// <summary>
/// Implementation of <see cref="ICurrentWeatherApi"/>
/// </summary>
public sealed class CurrentWeatherApi : AbstractApiImplBase, ICurrentWeatherApi
{
  private readonly string _apiKey;

  private readonly ICurrentWeatherApiClient _weatherApi;
  private readonly IGeocodingApiClient _geoApi;

  internal CurrentWeatherApi(string apiKey, IOpenWeatherMapOptions? options) : base(options)
  {
    _apiKey = apiKey;
    _weatherApi = RestService.For<ICurrentWeatherApiClient>(BaseUrl);
    _geoApi = RestService.For<IGeocodingApiClient>(BaseUrl);
  }

  /// <inheritdoc />
  public async Task<CurrentWeather?> QueryAsync(string query)
  {
    if (query == null) throw new ArgumentNullException(nameof(query));

    return await Cached(() => $"WeatherByName_{query}", async () =>
    {
      var response = await _geoApi.GeoCodeByLocationName(_apiKey, query, 1);
      if (!response.IsSuccessStatusCode)
      {
        throw new ApiException(response.StatusCode, response.ReasonPhrase, response.Error);
      }

      if (response.Content == null || !response.Content.Any())
      {
        return null;
      }

      var geoCode = response.Content.First();
      return MapResponse(
        await _weatherApi.CurrentWeather(_apiKey, Language, geoCode.Latitude, geoCode.Longitude)
      );
    });
  }

  /// <inheritdoc />
  public async Task<CurrentWeather?> QueryAsync(double lat, double lon)
  {
    return await Cached(
      () => $"WeatherByCoordinates_{lat}_{lon}",
      async () => MapResponse(await _weatherApi.CurrentWeather(_apiKey, Language, lat, lon))
    );
  }

  /// <inheritdoc />
  public async Task<CurrentWeather?> QueryAsync(int cityId)
  {
    return await Cached(
      () => $"WeatherByCityId_{cityId}",
      async () => MapResponse(await _weatherApi.CurrentWeather(_apiKey, Language, cityId))
    );
  }

  private static CurrentWeather? MapResponse(IApiResponse<ApiWeatherResponse> response)
  {
    if (!response.IsSuccessStatusCode)
    {
      throw new ApiException(response.StatusCode, response.ReasonPhrase, response.Error);
    }

    return response.Content?.ToWeather();
  }
}