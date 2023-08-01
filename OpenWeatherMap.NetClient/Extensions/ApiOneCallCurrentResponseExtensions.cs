using OpenWeatherMap.NetClient.Models;
using OpenWeatherMap.NetClient.RestApis.Responses;

namespace OpenWeatherMap.NetClient.Extensions;

internal static class ApiOneCallCurrentResponseExtensions
{
  internal static OneCallCurrentWeather ToWeather(this ApiOneCallCurrentResponse response)
  {
    return new OneCallCurrentWeather
    {
      FetchedTimeStamp = DateTimeOffset.UtcNow,
      Latitude = response.Latitude,
      Longitude = response.Longitude,
      TimeZone = response.TimeZone,
      TimeZoneOffset = response.TimeZoneOffset,
      Current = response.Current is null
        ? null
        : new OneCallCurrentWeather.CurrentWeather
        {
          MeasuredTimeStamp = response.Current.DataTimeStamp,
          Sunrise = response.Current.Sunrise,
          Sunset = response.Current.Sunset,
          Temperature = response.Current.Temperature,
          TemperatureFeelsLike = response.Current.TemperatureFeelsLike,
          Pressure = response.Current.Pressure,
          Humidity = response.Current.Humidity,
          Clouds = response.Current.Clouds,
          Visibility = response.Current.Visibility,
          WindSpeed = response.Current.WindSpeed,
          WindDirection = response.Current.WindDirection,
          WindGust = response.Current.WindGust,
          RainLastHour = response.Current.Rain?.OneHour,
          RainLastThreeHours = response.Current.Rain?.ThreeHours,
          SnowLastHour = response.Current.Snow?.OneHour,
          SnowLastThreeHours = response.Current.Snow?.ThreeHours,
          WeatherConditionId = response.Current.Weather.First().Id,
          WeatherCondition = response.Current.Weather.First().Main,
          WeatherDescription = response.Current.Weather.First().Description,
          WeatherIcon = response.Current.Weather.First().Icon
        },
      Minutely = response.MinuteForecast is null
        ? Enumerable.Empty<OneCallCurrentWeather.MinuteForecast>()
        : response.MinuteForecast.Select(minute => new OneCallCurrentWeather.MinuteForecast
        {
          TimeStamp = minute.TimeStamp,
          Precipitation = minute.Precipitation
        }),
      Hourly = response.HourForecast is null
        ? Enumerable.Empty<OneCallCurrentWeather.HourForecast>()
        : response.HourForecast.Select(hour => new OneCallCurrentWeather.HourForecast
        {
          TimeStamp = hour.TimeStamp,
          Temperature = hour.Temperature,
          TemperatureFeelsLike = hour.TemperatureFeelsLike,
          Pressure = hour.Pressure,
          Humidity = hour.Humidity,
          Clouds = hour.Clouds,
          Visibility = hour.Visibility,
          WindSpeed = hour.WindSpeed,
          WindDirection = hour.WindDirection,
          WindGust = hour.WindGust,
          ProbabilityPrecipitation = hour.ProbabilityPrecipitation,
          Rain = hour.Rain?.OneHour,
          Snow = hour.Snow?.OneHour,
          WeatherConditionId = hour.Weather.First().Id,
          WeatherCondition = hour.Weather.First().Main,
          WeatherDescription = hour.Weather.First().Description,
          WeatherIcon = hour.Weather.First().Icon
        }),
      Daily = response.DayForecast is null
        ? Enumerable.Empty<OneCallCurrentWeather.DayForecast>()
        : response.DayForecast.Select(day => new OneCallCurrentWeather.DayForecast
        {
          TimeStamp = day.TimeStamp,
          Sunrise = day.Sunrise,
          Sunset = day.Sunset,
          Moonrise = day.Moonrise,
          Moonset = day.MoonSet,
          Summary = day.Summary,
          TemperatureMorning = day.Temperature.Morning,
          TemperatureDay = day.Temperature.Day,
          TemperatureEvening = day.Temperature.Evening,
          TemperatureNight = day.Temperature.Night,
          TemperatureMin = day.Temperature.Min!.Value,
          TemperatureMax = day.Temperature.Max!.Value,
          TemperatureMorningFeelsLike = day.TemperatureFeelsLike.Morning,
          TemperatureDayFeelsLike = day.TemperatureFeelsLike.Day,
          TemperatureEveningFeelsLike = day.TemperatureFeelsLike.Evening,
          TemperatureNightFeelsLike = day.TemperatureFeelsLike.Night,
          Pressure = day.Pressure,
          Humidity = day.Humidity,
          Clouds = day.Clouds,
          WindSpeed = day.WindSpeed,
          WindDirection = day.WindDirection,
          WindGust = day.WindGust,
          ProbabilityPrecipitation = day.ProbabilityPrecipitation,
          Rain = day.Rain,
          Snow = day.Snow,
          WeatherConditionId = day.Weather.First().Id,
          WeatherCondition = day.Weather.First().Main,
          WeatherDescription = day.Weather.First().Description,
          WeatherIcon = day.Weather.First().Icon
        }),
      Alerts = response.Alerts.Select(alert => new OneCallCurrentWeather.Alert
      {
        Sender = alert.SenderName,
        Event = alert.Event,
        Description = alert.Description,
        Start = alert.Start,
        End = alert.End
      })
    };
  }
}