using OpenWeatherMap.NetClient.Models;
using OpenWeatherMap.NetClient.RestApis.Responses;

namespace OpenWeatherMap.NetClient.Extensions;

internal static class ApiGeoCodeResponseExtensions
{
  internal static GeoCode ToGeoCode(this ApiGeoCodeResponse response)
  {
    return new GeoCode
    {
      Name = response.Name,
      LocalNames = response.LocalNames,
      Latitude = response.Latitude,
      Longitude = response.Longitude,
      Country = response.Country,
    };
  }
}