using OpenWeatherMap.NetClient.Extensions;
using OpenWeatherMap.NetClient.Models;
using OpenWeatherMap.NetClient.RestApis.Clients;

namespace OpenWeatherMap.NetClient.Apis;

/// <summary>
/// Implementation of <see cref="IGeocodingApi"/>
/// </summary>
public sealed class GeocodingApi : IGeocodingApi
{
  private const string BaseUrl = "https://api.openweathermap.org";

  private readonly string _apiKey;

  private readonly RestClient<IGeocodingApiClient> _geoApi;

  internal GeocodingApi(string apiKey, OpenWeatherMapOptions options)
  {
    _apiKey = apiKey;
    _geoApi = new RestClient<IGeocodingApiClient>(BaseUrl, options);
  }

  /// <inheritdoc />
  public async Task<IEnumerable<GeoCode>> QueryAsync(string query, int limit = int.MaxValue)
  {
    if (query == null) throw new ArgumentNullException(nameof(query));

    return await _geoApi.Call(async api =>
      {
        var geoCodes = await api.GeoCodeByLocationName(_apiKey, query, limit);
        return geoCodes.Select(gc => gc.ToGeoCode());
      },
      () => $"GeoCode_{query}_{limit}"
    );
  }

  /// <inheritdoc />
  public async Task<IEnumerable<GeoCode>> QueryReverseAsync(double lat, double lon,
    int limit = int.MaxValue)
  {
    return await _geoApi.Call(async api =>
      {
        var geoCodes = await api.GeoCodeReverse(_apiKey, lat, lon, limit);
        return geoCodes.Select(gc => gc.ToGeoCode());
      },
      () => $"GeoCodeReverse_{lat}_{lon}_{limit}"
    );
  }
}