using OpenWeatherMap.NetClient.Models;
using OpenWeatherMap.NetClient.RestApis.Clients;

namespace OpenWeatherMap.NetClient.Apis;

/// <summary>
/// Implementation of <see cref="IBasicWeatherMapsApi"/>
/// </summary>
public sealed class BasicWeatherMapsApi : IBasicWeatherMapsApi
{
  private const string BaseUrl = "https://tile.openweathermap.org";

  private readonly string _apiKey;

  private readonly RestClient<IWeatherMapsApiClient> _client;

  internal BasicWeatherMapsApi(string apiKey, IOpenWeatherMapOptions? options)
  {
    _apiKey = apiKey;
    _client = new RestClient<IWeatherMapsApiClient>(BaseUrl, options);
  }

  /// <inheritdoc />
  public async Task<byte[]> GetMapAsync(BasicWeatherMapLayer layer, int zoom, int x, int y)
  {
    return await _client.Call(async api =>
    {
      var response = await api.GetWeatherMap(_apiKey, LayerToKey(layer), zoom, x, y);
      return await response.Content.ReadAsByteArrayAsync();
    });
  }

  private static string LayerToKey(BasicWeatherMapLayer layer)
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