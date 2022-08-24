using OpenWeatherMap.NetClient.Models;
using OpenWeatherMap.NetClient.RestApis.Clients;
using Refit;
using ApiException = OpenWeatherMap.NetClient.Exceptions.ApiException;


namespace OpenWeatherMap.NetClient.Apis;

/// <summary>
/// Implementation of <see cref="IWeatherMapsApi"/>
/// </summary>
public class WeatherMapsApi : IWeatherMapsApi
{
  private const string WeatherMapBaseUrl = "https://tile.openweathermap.org";
  private readonly string _apiKey;

  private readonly IWeatherMapsApiClient _weatherMapsApiClient;

  internal WeatherMapsApi(string apiKey, IOpenWeatherMapOptions? options)
  {
    _apiKey = apiKey;
    _weatherMapsApiClient = RestService.For<IWeatherMapsApiClient>(WeatherMapBaseUrl);
  }

  /// <inheritdoc />
  public async Task<byte[]> GetMapAsync(string layer, int zoom, int x, int y)
  {
    var response = await _weatherMapsApiClient.GetWeatherMap(_apiKey, layer, zoom, x, y);
    if (!response.IsSuccessStatusCode)
    {
      throw new ApiException(response.StatusCode, response.ReasonPhrase);
    }

    return await response.Content.ReadAsByteArrayAsync();
  }
}