using OpenWeatherMap.Net.Models;

namespace OpenWeatherMap.Net.Extensions;

internal static class ApiWeatherResponseExtensions
{
  internal static CurrentWeather ToWeather(this ApiWeatherResponse response)
  {
    var weather = response.Weather.First();
    return new CurrentWeather
    {
      MeasuredTimeStamp = response.DataTimeStamp,
      FetchedTimeStamp = DateTime.UtcNow,
      CityId = response.CityId,
      CityName = response.CityName,
      Country = response.Sys.Country,
      Longitude = response.Coordinates.Longitude,
      Latitude = response.Coordinates.Latitude,
      TimeZoneOffset = response.TimeZoneOffset,
      Sunrise = response.Sys.Sunrise,
      Sunset = response.Sys.Sunset,
      WeatherConditionId = weather.Id,
      WeatherCondition = weather.Main,
      WeatherDescription = weather.Description,
      WeatherIcon = weather.Icon,
      Visibility = response.Visibility,
      Temperature = response.Main.Temperature,
      TemperatureFeelsLike = response.Main.TemperatureFeelsLike,
      TemperatureMin = response.Main.TemperatureMin,
      TemperatureMax = response.Main.TemperatureMax,
      Pressure = response.Main.Pressure,
      Humidity = response.Main.Humidity,
      WindSpeed = response.Wind.Speed,
      WindDirection = response.Wind.Direction,
      Cloudiness = response.Clouds.All,
      RainLastHour = response.Rain?.OneHour,
      RainLastThreeHours = response.Rain?.ThreeHours,
      SnowLastHour = response.Snow?.OneHour,
      SnowLastThreeHours = response.Snow?.ThreeHours
    };
  }
}