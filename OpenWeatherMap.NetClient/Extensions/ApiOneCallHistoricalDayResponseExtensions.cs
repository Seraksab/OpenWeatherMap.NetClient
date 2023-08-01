using OpenWeatherMap.NetClient.Models;
using OpenWeatherMap.NetClient.RestApis.Responses;

namespace OpenWeatherMap.NetClient.Extensions;

internal static class ApiOneCallHistoricalDayResponseExtensions
{
  internal static OneCallHistoricalDayWeather ToWeather(this ApiOneCallHistoricalDayResponse response)
  {
    return new OneCallHistoricalDayWeather
    {
      FetchedTimeStamp = DateTimeOffset.UtcNow,
      Latitude = response.Latitude,
      Longitude = response.Longitude,
      TimeZone = response.TimeZone,
      Date = response.Date,
      Clouds = response.CloudCover.Afternoon,
      Humidity = response.Humidity.Afternoon,
      Precipitation = response.Precipitation.Total,
      Pressure = response.Pressure.Afternoon,
      TemperatureMin = response.Temperature.Min,
      TemperatureMax = response.Temperature.Max,
      TemperatureMorning = response.Temperature.Morning,
      TemperatureAfternoon = response.Temperature.Afternoon,
      TemperatureEvening = response.Temperature.Evening,
      TemperatureNight = response.Temperature.Night,
      WindSpeed = response.Wind.Max.Speed,
      WindDirection = response.Wind.Max.Direction,
    };
  }
}