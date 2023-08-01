using OpenWeatherMap.NetClient.Models;
using OpenWeatherMap.NetClient.RestApis.Responses;

namespace OpenWeatherMap.NetClient.Extensions;

internal static class ApiOneCallHistoricalResponseExtensions
{
  internal static OneCallHistoricalWeather ToWeather(this ApiOneCallHistoricalResponse response)
  {
    var data = response.Data.First();
    var weather = data.Weather.First();
    return new OneCallHistoricalWeather
    {
      FetchedTimeStamp = DateTimeOffset.UtcNow,
      Latitude = response.Latitude,
      Longitude = response.Longitude,
      TimeZone = response.TimeZone,
      TimeZoneOffset = response.TimeZoneOffset,
      TimeStamp = data.TimeStamp,
      Sunrise = data.Sunrise,
      Sunset = data.Sunset,
      Temperature = data.Temperature,
      TemperatureFeelsLike = data.TemperatureFeelsLike,
      Pressure = data.Pressure,
      Humidity = data.Humidity,
      Clouds = data.Clouds,
      Visibility = data.Visibility,
      WindSpeed = data.WindSpeed,
      WindDirection = data.WindDirection,
      WindGust = data.WindGust,
      Rain = data.Rain?.OneHour,
      Snow = data.Snow?.OneHour,
      WeatherConditionId = weather.Id,
      WeatherCondition = weather.Main,
      WeatherDescription = weather.Description,
      WeatherIcon = weather.Icon
    };
  }
}