using OpenWeatherMap.NetClient.Extensions;
using OpenWeatherMap.NetClient.Models;
using OpenWeatherMap.NetClient.RestApis.Clients;
using OpenWeatherMap.NetClient.RestApis.Responses;
using Refit;

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
  public async Task<Models.IApiResponse<AirPollution>> QueryCurrentAsync(double lat, double lon)
  {
    return await Cached<AirPollution>(
      () => $"current_{lat}_{lon}",
      async () =>
      {
        var response = await _airPollutionApiClient.Current(_apiKey, lat, lon);
        return new Models.ApiResponse<AirPollution>(
          response.StatusCode,
          response.ReasonPhrase,
          MapModels(response).FirstOrDefault(),
          response.Error
        );
      }
    );
  }

  /// <inheritdoc />
  public async Task<Models.IApiResponse<IEnumerable<AirPollution>>> QueryForecastAsync(double lat, double lon)
  {
    return await Cached<IEnumerable<AirPollution>>(
      () => $"forecast_{lat}_{lon}",
      async () =>
      {
        var response = await _airPollutionApiClient.Forecast(_apiKey, lat, lon);
        return new Models.ApiResponse<IEnumerable<AirPollution>>(
          response.StatusCode,
          response.ReasonPhrase,
          MapModels(response),
          response.Error
        );
      }
    );
  }

  /// <inheritdoc />
  public async Task<Models.IApiResponse<IEnumerable<AirPollution>>> QueryHistoricalAsync(double lat, double lon,
    DateTime start, DateTime end)
  {
    return await Cached<IEnumerable<AirPollution>>(
      () => $"historical_{lat}_{lon}_{start}_{end}",
      async () =>
      {
        var response = await _airPollutionApiClient.Historical(
          _apiKey, lat, lon,
          ((DateTimeOffset)start).ToUnixTimeSeconds(),
          ((DateTimeOffset)end).ToUnixTimeSeconds()
        );
        return new Models.ApiResponse<IEnumerable<AirPollution>>(
          response.StatusCode,
          response.ReasonPhrase,
          MapModels(response),
          response.Error
        );
      }
    );
  }

  private static IEnumerable<AirPollution> MapModels(
    Refit.IApiResponse<ApiAirPollutionResponse> response)
  {
    return response.Content == null
      ? Enumerable.Empty<AirPollution>()
      : response.Content.List.Select(e => e.ToAirPollution());
  }
}