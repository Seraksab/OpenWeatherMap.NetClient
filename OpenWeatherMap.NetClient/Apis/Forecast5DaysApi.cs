using OpenWeatherMap.NetClient.Extensions;
using OpenWeatherMap.NetClient.Models;
using OpenWeatherMap.NetClient.RestApis.Clients;
using OpenWeatherMap.NetClient.RestApis.Responses;
using Refit;
using ApiException = OpenWeatherMap.NetClient.Exceptions.ApiException;

namespace OpenWeatherMap.NetClient.Apis;

/// <summary>
/// Implementation of <see cref="IForecast5DaysApi"/>
/// </summary>
public sealed class Forecast5DaysApi : AbstractApiImplBase, IForecast5DaysApi
{
  private readonly string _apiKey;

  private readonly IForecast5DaysApiClient _forecastApi;
  private readonly IGeocodingApiClient _geoApi;

  internal Forecast5DaysApi(string apiKey, IOpenWeatherMapOptions? options) : base(options)
  {
    _apiKey = apiKey;
    _forecastApi = RestService.For<IForecast5DaysApiClient>(BaseUrl);
    _geoApi = RestService.For<IGeocodingApiClient>(BaseUrl);
  }

  /// <inheritdoc />
  public async Task<Forecast5Days?> QueryAsync(string query, int limit = int.MaxValue)
  {
    if (query == null) throw new ArgumentNullException(nameof(query));

    return await Cached(() => $"Forecast5DaysByName_{query}", async () =>
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
        await _forecastApi.Forecast(_apiKey, Language, geoCode.Latitude, geoCode.Longitude, limit)
      );
    });
  }

  /// <inheritdoc />
  public async Task<Forecast5Days?> GetByCoordinatesAsync(double lat, double lon, int limit = int.MaxValue)
  {
    return await Cached(
      () => $"Forecast5DaysByCoordinates_{lat}_{lon}",
      async () => MapResponse(await _forecastApi.Forecast(_apiKey, Language, lat, lon, limit))
    );
  }

  /// <inheritdoc />
  public async Task<Forecast5Days?> GetByCityIdAsync(int cityId, int limit = int.MaxValue)
  {
    return await Cached(
      () => $"Forecast5DaysByCityId_{cityId}",
      async () => MapResponse(await _forecastApi.Forecast(_apiKey, Language, cityId, limit))
    );
  }

  private static Forecast5Days? MapResponse(IApiResponse<ApiForecast5DaysResponse> response)
  {
    if (!response.IsSuccessStatusCode)
    {
      throw new ApiException(response.StatusCode, response.ReasonPhrase, response.Error);
    }

    return response.Content?.ToForecast();
  }
}