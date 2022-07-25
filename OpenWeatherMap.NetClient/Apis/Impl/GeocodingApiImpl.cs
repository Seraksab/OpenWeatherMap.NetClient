using OpenWeatherMap.NetClient.ApiClients;
using OpenWeatherMap.NetClient.Extensions;
using OpenWeatherMap.NetClient.Models;
using OpenWeatherMap.NetClient.Models.ApiResponses;
using Refit;

namespace OpenWeatherMap.NetClient.Apis.Impl;

internal class GeocodingApiImpl : AbstractApiImplBase, IGeocodingApi
{
  private readonly string _apiKey;

  private readonly IGeocodingApiClient _geoCodingApiClient;

  internal GeocodingApiImpl(string apiKey, IOpenWeatherMapOptions? options) : base(options)
  {
    _apiKey = apiKey;
    _geoCodingApiClient = RestService.For<IGeocodingApiClient>(BaseUrl);;
  }

  public async Task<Models.IApiResponse<IEnumerable<GeoCode>>> QueryAsync(string query, int limit = Int32.MaxValue)
  {
    return await CacheRequest($"GeoCode_{query}_{limit}", async () =>
      MapGeoCodes(await _geoCodingApiClient.GeoCodeByLocationName(_apiKey, query, limit))
    );
  }

  public async Task<Models.IApiResponse<IEnumerable<GeoCode>>> QueryReverseAsync(double lat, double lon,
    int limit = Int32.MaxValue)
  {
    return await CacheRequest($"GeoCodeReverse_{lat}_{lon}_{limit}",
      async () => MapGeoCodes(await _geoCodingApiClient.GeoCodeReverse(_apiKey, lat, lon, limit))
    );
  }

  private static Models.IApiResponse<IEnumerable<GeoCode>> MapGeoCodes(
    Refit.IApiResponse<ApiGeoCodeResponse[]> response)
  {
    var mappedResponse = response.Content == null
      ? Enumerable.Empty<GeoCode>()
      : response.Content.Select(gc => gc.ToGeoCode());
    return new Models.ApiResponse<IEnumerable<GeoCode>>(
      response.StatusCode,
      response.ReasonPhrase,
      mappedResponse,
      response.Error
    );
  }
}