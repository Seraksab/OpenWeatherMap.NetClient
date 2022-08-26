using OpenWeatherMap.NetClient.Models;
using OpenWeatherMap.NetClient.RestApis.Clients;
using Refit;
using ApiException = OpenWeatherMap.NetClient.Exceptions.ApiException;


namespace OpenWeatherMap.NetClient.Apis;

/// <summary>
/// Implementation of <see cref="IBasicWeatherMapsApi"/>
/// </summary>
public sealed class BasicWeatherMapsApi : IBasicWeatherMapsApi
{
  private const string WeatherMapBaseUrl = "https://tile.openweathermap.org";
  private readonly string _apiKey;

  private readonly IWeatherMapsApiClient _weatherMapsApiClient;

  internal BasicWeatherMapsApi(string apiKey, IOpenWeatherMapOptions? options)
  {
    _apiKey = apiKey;
    _weatherMapsApiClient = RestService.For<IWeatherMapsApiClient>(WeatherMapBaseUrl);
  }

  /// <inheritdoc />
  public async Task<byte[]> GetMapAsync(BasicWeatherMapLayer layer, int zoom, int x, int y)
  {
    var response = await _weatherMapsApiClient.GetWeatherMap(_apiKey, LayerToApiKey(layer), zoom, x, y);
    if (!response.IsSuccessStatusCode)
    {
      throw new ApiException(response.StatusCode, response.ReasonPhrase);
    }

    return await response.Content.ReadAsByteArrayAsync();
  }

  private static string LayerToApiKey(BasicWeatherMapLayer layer)
  {
    return layer switch
    {
      BasicWeatherMapLayer.Clouds => "clouds_new",
      BasicWeatherMapLayer.Precipitation => "precipitation_new",
      BasicWeatherMapLayer.SeaLevelPressure => "pressure_new",
      BasicWeatherMapLayer.WindSpeed => "wind_new",
      BasicWeatherMapLayer.Temperature => "temp_new",
      _ => throw new ArgumentOutOfRangeException(nameof(layer), layer, null)
    };
  }
}