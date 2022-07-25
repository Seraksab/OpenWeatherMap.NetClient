using OpenWeatherMap.Net.Models;
using OpenWeatherMap.Net.Models.ApiResponses;

namespace OpenWeatherMap.Net.Extensions;

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