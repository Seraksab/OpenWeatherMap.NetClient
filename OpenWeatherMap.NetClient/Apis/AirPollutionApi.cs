using OpenWeatherMap.NetClient.Extensions;
using OpenWeatherMap.NetClient.Models;
using OpenWeatherMap.NetClient.RestApis.Clients;
using OpenWeatherMap.NetClient.RestApis.Responses;
using Refit;
using ApiException = OpenWeatherMap.NetClient.Exceptions.ApiException;

namespace OpenWeatherMap.NetClient.Apis;

/// <summary>
/// Implementation of <see cref="IAirPollutionApi"/>
/// </summary>
public sealed class AirPollutionApi : AbstractApiImplBase, IAirPollutionApi
{
  private readonly string _apiKey;

  private readonly IAirPollutionApiClient _airPollutionApiClient;

  internal AirPollutionApi(string apiKey, IOpenWeatherMapOptions? options) : base(options)
  {
    _apiKey = apiKey;
    _airPollutionApiClient = RestService.For<IAirPollutionApiClient>(BaseUrl);
  }

  /// <inheritdoc />
  public async Task<AirPollution?> QueryCurrentAsync(double lat, double lon)
  {
    return await Cached(
      () => $"current_{lat}_{lon}",
      async () =>
      {
        var response = await _airPollutionApiClient.Current(_apiKey, lat, lon);
        if (!response.IsSuccessStatusCode)
        {
          throw new ApiException(response.StatusCode, response.ReasonPhrase, response.Error);
        }

        return response.Content == null ? null : MapModels(response).FirstOrDefault();
      }
    );
  }

  /// <inheritdoc />
  public async Task<IEnumerable<AirPollution>> QueryForecastAsync(double lat, double lon)
  {
    return await Cached(
      () => $"forecast_{lat}_{lon}",
      async () =>
      {
        var response = await _airPollutionApiClient.Forecast(_apiKey, lat, lon);
        if (!response.IsSuccessStatusCode)
        {
          throw new ApiException(response.StatusCode, response.ReasonPhrase, response.Error);
        }

        return MapModels(response);
      }
    );
  }

  /// <inheritdoc />
  public async Task<IEnumerable<AirPollution>> QueryHistoricalAsync(double lat, double lon,
    DateTime start, DateTime end)
  {
    return await Cached(
      () => $"historical_{lat}_{lon}_{start}_{end}",
      async () =>
      {
        var response = await _airPollutionApiClient.Historical(
          _apiKey, lat, lon,
          ((DateTimeOffset)start).ToUnixTimeSeconds(),
          ((DateTimeOffset)end).ToUnixTimeSeconds()
        );

        if (!response.IsSuccessStatusCode)
        {
          throw new ApiException(response.StatusCode, response.ReasonPhrase, response.Error);
        }

        return MapModels(response);
      }
    );
  }

  private static IEnumerable<AirPollution> MapModels(IApiResponse<ApiAirPollutionResponse> response)
  {
    return response.Content == null
      ? Enumerable.Empty<AirPollution>()
      : response.Content.List.Select(e => e.ToAirPollution());
  }
}