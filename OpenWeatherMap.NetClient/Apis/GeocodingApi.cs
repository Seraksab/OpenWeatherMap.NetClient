using OpenWeatherMap.NetClient.Exceptions;
using OpenWeatherMap.NetClient.Extensions;
using OpenWeatherMap.NetClient.Models;
using OpenWeatherMap.NetClient.RestApis.Clients;
using OpenWeatherMap.NetClient.RestApis.Responses;
using Refit;
using ApiException = OpenWeatherMap.NetClient.Exceptions.ApiException;

namespace OpenWeatherMap.NetClient.Apis;

/// <summary>
/// Implementation of <see cref="IGeocodingApi"/>
/// </summary>
public sealed class GeocodingApi : AbstractApiImplBase, IGeocodingApi
{
  private readonly string _apiKey;

  private readonly IGeocodingApiClient _geoCodingApiClient;

  internal GeocodingApi(string apiKey, IOpenWeatherMapOptions? options) : base(options)
  {
    _apiKey = apiKey;
    _geoCodingApiClient = RestService.For<IGeocodingApiClient>(BaseUrl);
  }

  /// <inheritdoc />
  public async Task<IEnumerable<GeoCode>> QueryAsync(string query, int limit = int.MaxValue)
  {
    if (query == null) throw new ArgumentNullException(nameof(query));

    return await Cached(() => $"GeoCode_{query}_{limit}", async () =>
      MapGeoCodes(await _geoCodingApiClient.GeoCodeByLocationName(_apiKey, query, limit))
    );
  }

  /// <inheritdoc />
  public async Task<IEnumerable<GeoCode>> QueryReverseAsync(double lat, double lon,
    int limit = int.MaxValue)
  {
    return await Cached(() => $"GeoCodeReverse_{lat}_{lon}_{limit}",
      async () => MapGeoCodes(await _geoCodingApiClient.GeoCodeReverse(_apiKey, lat, lon, limit))
    );
  }

  private static IEnumerable<GeoCode> MapGeoCodes(IApiResponse<ApiGeoCodeResponse[]> response)
  {
    if (!response.IsSuccessStatusCode)
    {
      throw new ApiException(response.StatusCode, response.ReasonPhrase, response.Error);
    }

    return response.Content == null
      ? Enumerable.Empty<GeoCode>()
      : response.Content.Select(gc => gc.ToGeoCode());
  }
}