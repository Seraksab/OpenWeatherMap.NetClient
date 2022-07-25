using OpenWeatherMap.Net.ApiClients;
using OpenWeatherMap.Net.Extensions;
using OpenWeatherMap.Net.Models;
using OpenWeatherMap.Net.Models.ApiResponses;
using Refit;

namespace OpenWeatherMap.Net.Apis.Impl;

internal class AirPollutionApiImpl : AbstractApiImplBase, IAirPollutionApi
{
  private readonly string _apiKey;

  private readonly IAirPollutionApiClient _airPollutionApiClient;

  public AirPollutionApiImpl(string apiKey, IOpenWeatherMapOptions? options) : base(options)
  {
    _apiKey = apiKey;
    _airPollutionApiClient = RestService.For<IAirPollutionApiClient>(BaseUrl);
  }

  public async Task<Models.IApiResponse<AirPollution>> QueryCurrentAsync(double lat, double lon)
  {
    var response = await _airPollutionApiClient.Current(_apiKey, lat, lon);
    return new Models.ApiResponse<AirPollution>(
      response.StatusCode,
      response.ReasonPhrase,
      MapModels(response).FirstOrDefault(),
      response.Error
    );
  }

  public async Task<Models.IApiResponse<IEnumerable<AirPollution>>> QueryForecastAsync(double lat, double lon)
  {
    var response = await _airPollutionApiClient.Forecast(_apiKey, lat, lon);
    return new Models.ApiResponse<IEnumerable<AirPollution>>(
      response.StatusCode,
      response.ReasonPhrase,
      MapModels(response),
      response.Error
    );
  }

  public async Task<Models.IApiResponse<IEnumerable<AirPollution>>> QueryHistoricalAsync(double lat, double lon,
    DateTime start, DateTime end)
  {
    var response = await _airPollutionApiClient.Historical(_apiKey, lat, lon,
      ((DateTimeOffset)start).ToUnixTimeSeconds(), ((DateTimeOffset)end).ToUnixTimeSeconds());
    return new Models.ApiResponse<IEnumerable<AirPollution>>(
      response.StatusCode,
      response.ReasonPhrase,
      MapModels(response),
      response.Error
    );
  }

  private static IEnumerable<AirPollution> MapModels(
    Refit.IApiResponse<ApiAirPollutionResponse> response)
  {
    return response.Content == null
      ? Enumerable.Empty<AirPollution>()
      : response.Content.Elements.Select(e => e.ToAirPollution());
  }
}