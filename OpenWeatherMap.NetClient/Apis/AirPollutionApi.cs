using OpenWeatherMap.NetClient.Extensions;
using OpenWeatherMap.NetClient.Models;
using OpenWeatherMap.NetClient.RestApis.Clients;
using OpenWeatherMap.NetClient.RestApis.Responses;

namespace OpenWeatherMap.NetClient.Apis;

/// <summary>
/// Implementation of <see cref="IAirPollutionApi"/>
/// </summary>
public sealed class AirPollutionApi : IAirPollutionApi
{
  private const string BaseUrl = "https://api.openweathermap.org";

  private readonly string _apiKey;

  private readonly HttpClient _httpClient;
  private readonly RestClient<IAirPollutionApiClient> _client;

  internal AirPollutionApi(string apiKey, OpenWeatherMapOptions options)
  {
    _apiKey = apiKey;
    _httpClient = new HttpClient
    {
      BaseAddress = new Uri(BaseUrl)
    };
    _client = new RestClient<IAirPollutionApiClient>(Client, options);
  }

  /// <inheritdoc />
  public HttpClient Client => _httpClient;

  /// <inheritdoc />
  public async Task<AirPollution?> GetCurrentAsync(double lat, double lon)
  {
    return await _client.Call(async api =>
      {
        var result = await api.Current(_apiKey, lat, lon);
        return MapModels(result).FirstOrDefault();
      },
      () => $"current_{lat}_{lon}"
    );
  }

  /// <inheritdoc />
  public async Task<IEnumerable<AirPollution>> GetForecastAsync(double lat, double lon)
  {
    return await _client.Call(
      async api => MapModels(await api.Forecast(_apiKey, lat, lon)),
      () => $"forecast_{lat}_{lon}"
    );
  }

  /// <inheritdoc />
  public async Task<IEnumerable<AirPollution>> GetHistoricalAsync(double lat, double lon, DateTime start,
    DateTime end)
  {
    return await _client.Call(async api =>
      {
        var result = await api.Historical(
          _apiKey, lat, lon,
          ((DateTimeOffset)start).ToUnixTimeSeconds(),
          ((DateTimeOffset)end).ToUnixTimeSeconds()
        );
        return MapModels(result);
      },
      () => $"historical_{lat}_{lon}_{start}_{end}"
    );
  }

  private static IEnumerable<AirPollution> MapModels(ApiAirPollutionResponse response)
  {
    return response.List.Select(e => e.ToAirPollution());
  }
}