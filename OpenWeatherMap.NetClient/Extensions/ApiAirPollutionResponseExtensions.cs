using OpenWeatherMap.NetClient.Models;
using OpenWeatherMap.NetClient.Models.ApiResponses;

namespace OpenWeatherMap.NetClient.Extensions;

internal static class ApiAirPollutionResponseExtensions
{
  internal static AirPollution ToAirPollution(this ApiAirPollutionResponse.Element response)
  {
    return new AirPollution
    {
      TimeStamp = response.TimeStamp,
      AirQualityIndex = response.Main.AirQualityIndex,
      CarbonMonoxide = response.Components.CarbonMonoxide,
      NitrogenMonoxide = response.Components.NitrogenMonoxide,
      NitrogenDioxide = response.Components.NitrogenDioxide,
      Ozone = response.Components.Ozone,
      SulphurDioxide = response.Components.SulphurDioxide,
      FineParticlesMatter = response.Components.FineParticlesMatter,
      CoarseParticulateMatter = response.Components.CoarseParticulateMatter,
      Ammonia = response.Components.Ammonia
    };
  }
}