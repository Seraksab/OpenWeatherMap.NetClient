﻿using OpenWeatherMap.NetClient.Extensions;
using OpenWeatherMap.NetClient.Models;
using OpenWeatherMap.NetClient.RestApis.Clients;
using OpenWeatherMap.NetClient.RestApis.Responses;
using Refit;

namespace OpenWeatherMap.NetClient.Apis;

/// <summary>
/// Implementation of <see cref="ICurrentWeatherApi"/>
/// </summary>
public sealed class CurrentWeatherApiImpl : AbstractApiImplBase, ICurrentWeatherApi
{
  private readonly string _apiKey;

  private readonly ICurrentWeatherApiClient _weatherApi;
  private readonly IGeocodingApiClient _geoApi;

  public CurrentWeatherApiImpl(string apiKey, IOpenWeatherMapOptions? options) : base(options)
  {
    _apiKey = apiKey;
    _weatherApi = RestService.For<ICurrentWeatherApiClient>(BaseUrl);
    _geoApi = RestService.For<IGeocodingApiClient>(BaseUrl);
  }

  /// <inheritdoc />
  public async Task<Models.IApiResponse<CurrentWeather>> QueryAsync(string query)
  {
    if (query == null) throw new ArgumentNullException(nameof(query));

    return await CacheRequest(() => $"WeatherByName_{query}", async () =>
    {
      var response = await _geoApi.GeoCodeByLocationName(_apiKey, query, 1);
      if (!response.IsSuccessStatusCode || response.Content == null || !response.Content.Any())
      {
        return new Models.ApiResponse<CurrentWeather>(response.StatusCode, response.ReasonPhrase, null, response.Error);
      }

      var geoCode = response.Content.First();
      return MapResponse(
        await _weatherApi.CurrentWeather(_apiKey, Language, geoCode.Latitude, geoCode.Longitude)
      );
    });
  }

  /// <inheritdoc />
  public async Task<Models.IApiResponse<CurrentWeather>> QueryAsync(double lat, double lon)
  {
    return await CacheRequest(
      () => $"WeatherByCoordinates_{lat}_{lon}",
      async () => MapResponse(await _weatherApi.CurrentWeather(_apiKey, Language, lat, lon))
    );
  }

  /// <inheritdoc />
  public async Task<Models.IApiResponse<CurrentWeather>> QueryAsync(int cityId)
  {
    return await CacheRequest(
      () => $"WeatherByCityId_{cityId}",
      async () => MapResponse(await _weatherApi.CurrentWeather(_apiKey, Language, cityId))
    );
  }

  private Models.IApiResponse<CurrentWeather> MapResponse(Refit.IApiResponse<ApiWeatherResponse> response)
  {
    return new Models.ApiResponse<CurrentWeather>(
      response.StatusCode,
      response.ReasonPhrase,
      response.Content?.ToWeather(),
      response.Error
    );
  }
}