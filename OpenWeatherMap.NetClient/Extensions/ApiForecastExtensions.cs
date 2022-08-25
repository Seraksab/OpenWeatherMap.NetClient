using OpenWeatherMap.NetClient.Models;
using OpenWeatherMap.NetClient.RestApis.Responses;

namespace OpenWeatherMap.NetClient.Extensions;

internal static class ApiForecastExtensions
{
  internal static Forecast5Days ToForecast(this ApiForecast5DaysResponse response)
  {
    var forecast = response.List.Select(e =>
    {
      var weather = e.Weather.First();
      return new Forecast5Days.Weather
      {
        ForecastTimeStamp = e.ForecastTimeStamp,
        Temperature = e.Main.Temperature,
        TemperatureFeelsLike = e.Main.TemperatureFeelsLike,
        TemperatureMin = e.Main.TemperatureMin,
        TemperatureMax = e.Main.TemperatureMax,
        Pressure = e.Main.Pressure,
        PressureSeaLevel = e.Main.PressureSeaLevel,
        PressureGroundLevel = e.Main.PressureGroundLevel,
        Humidity = e.Main.Humidity,
        WeatherConditionId = weather.Id,
        WeatherCondition = weather.Main,
        WeatherDescription = weather.Description,
        WeatherIcon = weather.Icon,
        Cloudiness = e.Clouds.All,
        WindSpeed = e.Wind.Speed,
        WindDirection = e.Wind.Direction,
        WindGust = e.Wind.Gust,
        Visibility = e.Visibility,
        PrecipitationProbability = e.ProbabilityPrecipitation,
        RainLastHour = e.Rain?.OneHour,
        RainLastThreeHours = e.Rain?.ThreeHours,
        SnowLastHour = e.Snow?.OneHour,
        SnowLastThreeHours = e.Snow?.ThreeHours
      };
    });

    return new Forecast5Days
    {
      Forecast = forecast,
      CityId = response.City.Id,
      CityName = response.City.Name,
      Country = response.City.Country,
      Longitude = response.City.Coordinates.Longitude,
      Latitude = response.City.Coordinates.Latitude,
      Population = response.City.Population,
      TimeZoneOffset = response.City.TimeZoneOffset,
      Sunrise = response.City.Sunrise,
      Sunset = response.City.Sunset
    };
  }
}