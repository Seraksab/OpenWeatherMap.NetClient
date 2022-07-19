using System.Globalization;
using System.Text.RegularExpressions;
using OpenWeatherMap.Net.Api;
using OpenWeatherMap.Net.Models;
using Refit;

namespace OpenWeatherMap.Net;

public class OpenWeatherMap : IOpenWeatherMap
{
  private static readonly Regex ApiKeyRegex = new(@"^[0-9a-f]{32}$");

  private const string BaseUrl = "https://api.openweathermap.org";
  private const string DefaultLang = "en";

  private readonly string _apiKey;
  private readonly string _lang;
  private readonly IOpenWeatherMapApi _apiClient;

  public OpenWeatherMap(string apiKey, CultureInfo? culture = null)
  {
    if (apiKey == null) throw new ArgumentNullException(nameof(apiKey));
    if (!ApiKeyRegex.IsMatch(apiKey)) throw new ArgumentException($"'{apiKey}' is not a valid API key");

    _apiKey = apiKey;
    _lang = culture?.TwoLetterISOLanguageName ?? DefaultLang;
    _apiClient = RestService.For<IOpenWeatherMapApi>(BaseUrl);
  }

  public async Task<CurrentWeather?> CurrentWeatherByName(string name)
  {
    if (name == null) throw new ArgumentNullException(nameof(name));

    var result = await Request(() => _apiClient.CoordinatesByLocationName(_apiKey, 1, name));
    if (result == null || result.Length == 0) return null;
    return await CurrentWeatherByCoordinates(result[0].Latitude, result[0].Longitude);
  }

  public async Task<CurrentWeather?> CurrentWeatherByZip(string zip)
  {
    if (zip == null) throw new ArgumentNullException(nameof(zip));

    var result = await Request(() => _apiClient.CoordinatesByZipCode(_apiKey, zip));
    if (result == null) return null;
    return await CurrentWeatherByCoordinates(result.Latitude, result.Longitude);
  }


  public async Task<CurrentWeather?> CurrentWeatherByCoordinates(double lat, double lon)
  {
    var result = await Request(() => _apiClient.CurrentWeather(_apiKey, _lang, lat, lon));
    return MapData(result);
  }

  private async Task<T?> Request<T>(Func<Task<ApiResponse<T>>> apiFunc)
  {
    var response = await apiFunc();
    if (!response.IsSuccessStatusCode || response.Error != null)
    {
      throw new OpenWeatherMapException(response);
    }

    return response.Content;
  }

  private CurrentWeather? MapData(ApiWeatherResponse? response)
  {
    if (response == null) return null;

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