using System.Net;
using OpenWeatherMap.NetClient.Enums;
using OpenWeatherMap.NetClient.Models;
using Refit;

namespace OpenWeatherMap.NetClient.UnitTests;

public class Tests
{
  private const string ApiKey = "[API-KEY]";

  [Fact]
  public async Task TestInvalidApiKey()
  {
    // illegal api key format
    Assert.Throws<ArgumentException>(() => new OpenWeatherMapClient("foo"));

    // invalid api key => unauthorized
    var client = new OpenWeatherMapClient("00000000000000000000000000000000");
    var exception =
      await Assert.ThrowsAsync<ApiException>(() => client.CurrentWeather.QueryAsync("Linz,AT"));
    Assert.Equal(HttpStatusCode.Unauthorized, exception.StatusCode);
  }

  [Fact]
  public async Task TestCityNameNotFound()
  {
    var client = new OpenWeatherMapClient(ApiKey);
    var result = await client.CurrentWeather.QueryAsync("foobar");
    Assert.Null(result);
  }

  [Fact]
  public async Task TestCityName()
  {
    var client = new OpenWeatherMapClient(ApiKey);
    var result = await client.CurrentWeather.QueryAsync("Linz,AT");
    Assert.NotNull(result);
    Assert.Equal("Linz", result?.CityName);
    Assert.Equal("AT", result?.Country);
  }

  [Fact]
  public async Task TestNullCityName()
  {
    var client = new OpenWeatherMapClient(ApiKey);
    await Assert.ThrowsAsync<ArgumentNullException>(() => client.CurrentWeather.QueryAsync(null!));
  }

  [Fact]
  public async Task TestCoordinates()
  {
    var client = new OpenWeatherMapClient(ApiKey);
    var result = await client.CurrentWeather.GetByCoordinatesAsync(48.3059D, 14.2862D);
    Assert.NotNull(result);
    Assert.Equal("Linz", result?.CityName);
    Assert.Equal("AT", result?.Country);
  }

  [Fact]
  public async Task TestCityId()
  {
    var client = new OpenWeatherMapClient(ApiKey);
    var result = await client.CurrentWeather.GetByCityIdAsync(2772400);
    Assert.NotNull(result);
    Assert.Equal("Linz", result?.CityName);
    Assert.Equal("AT", result?.Country);
  }

  [Fact]
  public async Task TestGeoCode()
  {
    var client = new OpenWeatherMapClient(ApiKey);
    var result = await client.Geocoding.QueryAsync("Linz,AT");
    Assert.NotNull(result);
    var geoCodes = result as GeoCode[] ?? result.ToArray();
    Assert.NotEmpty(geoCodes);
    Assert.Equal("AT", geoCodes.First().Country);
    Assert.Equal("Linz", geoCodes.First().Name);
    Assert.Equal(48.3059, geoCodes.First().Latitude, 3);
    Assert.Equal(14.2862, geoCodes.First().Longitude, 3);
  }

  [Fact]
  public async Task TestGeoCodeReverse()
  {
    var client = new OpenWeatherMapClient(ApiKey);
    var result = await client.Geocoding.QueryReverseAsync(48.3059, 14.2862);
    Assert.NotNull(result);
    var geoCodes = result as GeoCode[] ?? result.ToArray();
    Assert.NotEmpty(geoCodes);
    Assert.Equal("AT", geoCodes.First().Country);
    Assert.Equal("Linz", geoCodes.First().Name);
  }

  [Fact]
  public async Task TestAirPollutionCurrent()
  {
    var client = new OpenWeatherMapClient(ApiKey);
    var result = await client.AirPollution.GetCurrentAsync(48.3059, 14.2862);
    Assert.NotNull(result);
  }

  [Fact]
  public async Task TestAirPollutionForecast()
  {
    var client = new OpenWeatherMapClient(ApiKey);
    var result = await client.AirPollution.GetForecastAsync(48.3059, 14.2862);
    Assert.NotNull(result);
    Assert.NotEmpty(result);
  }

  [Fact]
  public async Task TestAirPollutionHistory()
  {
    var client = new OpenWeatherMapClient(ApiKey);
    var to = DateTimeOffset.UtcNow;
    var from = to - TimeSpan.FromDays(1);
    var result = await client.AirPollution.GetHistoricalAsync(48.3059, 14.2862, from, to);
    Assert.NotNull(result);
    var airPollutions = result as AirPollution[] ?? result.ToArray();
    Assert.NotEmpty(airPollutions);
    Assert.All(airPollutions, ap => Assert.InRange(ap.TimeStamp, from, to));
  }

  [Fact]
  public async Task TestCache()
  {
    var cachedClient = new OpenWeatherMapClient(ApiKey, new OpenWeatherMapOptions
    {
      CacheDuration = TimeSpan.FromMinutes(1)
    });
    var firstResult = await cachedClient.CurrentWeather.GetByCoordinatesAsync(48.3059D, 14.2862D);
    var secondResult = await cachedClient.CurrentWeather.GetByCoordinatesAsync(48.3059D, 14.2862D);
    Assert.True(firstResult == secondResult);
  }

  [Fact]
  public async Task TestCacheDisabled()
  {
    var client = new OpenWeatherMapClient(ApiKey, new OpenWeatherMapOptions
    {
      CacheDuration = TimeSpan.Zero
    });
    var firstResult = await client.CurrentWeather.GetByCoordinatesAsync(48.3059D, 14.2862D);
    var secondResult = await client.CurrentWeather.GetByCoordinatesAsync(48.3059D, 14.2862D);
    Assert.True(firstResult != secondResult);
    Assert.True(firstResult!.FetchedTimeStamp != secondResult!.FetchedTimeStamp);
  }

  [Fact]
  public async Task TestWeatherMaps()
  {
    var client = new OpenWeatherMapClient(ApiKey);
    var map = await client.BasicWeatherMaps.GetMapAsync(BasicWeatherMapLayer.Clouds, 0, 0, 0);
    Assert.NotEmpty(map);
    Assert.True(map.Length > 1000);
  }

  [Fact]
  public async Task TestForecast5Days()
  {
    var client = new OpenWeatherMapClient(ApiKey);
    var forecast = await client.Forecast5Days.QueryAsync("Linz,AT");
    Assert.NotNull(forecast);
    Assert.Equal("AT", forecast.Country);
    Assert.Equal("Linz", forecast.CityName);
    Assert.Equal(48.3059, forecast.Latitude, 3);
    Assert.Equal(14.2862, forecast.Longitude, 3);
    Assert.Equal(40, forecast.Forecast.Count()); // 5 days worth with 3 hour steps

    // test limit
    forecast = await client.Forecast5Days.QueryAsync("Linz,AT", 2);
    Assert.NotNull(forecast);
    Assert.Equal(2, forecast.Forecast.Count());

    // by coordinates
    forecast = await client.Forecast5Days.GetByCoordinatesAsync(48.3059D, 14.2862D, 10);
    Assert.NotNull(forecast);
    Assert.Equal("AT", forecast.Country);
    Assert.Equal("Linz", forecast.CityName);
    Assert.Equal(48.3059, forecast.Latitude, 3);
    Assert.Equal(14.2862, forecast.Longitude, 3);
    Assert.Equal(10, forecast.Forecast.Count());

    // by city id
    forecast = await client.Forecast5Days.GetByCityIdAsync(2772400, 5);
    Assert.NotNull(forecast);
    Assert.Equal("AT", forecast.Country);
    Assert.Equal("Linz", forecast.CityName);
    Assert.Equal(48.3059, forecast.Latitude, 3);
    Assert.Equal(14.2862, forecast.Longitude, 3);
    Assert.Equal(5, forecast.Forecast.Count());
  }

  [Fact]
  public async Task TestOneCallCurrent()
  {
    var client = new OpenWeatherMapClient(ApiKey);

    // no exclude by city name
    var weather = await client.OneCall.QueryAsync("Linz,AT");
    Assert.NotNull(weather);
    Assert.Equal(48.3059, weather.Latitude, 3);
    Assert.Equal(14.2862, weather.Longitude, 3);
    Assert.Equal("Europe/Vienna", weather.TimeZone);
    Assert.NotNull(weather.Current);
    Assert.NotEmpty(weather.Minutely);
    Assert.NotEmpty(weather.Hourly);
    Assert.NotEmpty(weather.Daily);

    // no exclude by coordinates
    weather = await client.OneCall.GetByCoordinatesAsync(48.3059, 14.2862);
    Assert.NotNull(weather);
    Assert.Equal(48.3059, weather.Latitude, 3);
    Assert.Equal(14.2862, weather.Longitude, 3);
    Assert.Equal("Europe/Vienna", weather.TimeZone);
    Assert.NotNull(weather.Current);
    Assert.NotEmpty(weather.Minutely);
    Assert.NotEmpty(weather.Hourly);
    Assert.NotEmpty(weather.Daily); 

    // with exclude
    weather = await client.OneCall.QueryAsync(
      "Linz,AT", new[] { OneCallCategory.Daily, OneCallCategory.Current, OneCallCategory.Hourly }
    );
    Assert.NotNull(weather);
    Assert.Null(weather.Current);
    Assert.NotEmpty(weather.Minutely);
    Assert.Empty(weather.Hourly);
    Assert.Empty(weather.Daily);
  }

  [Fact]
  public async Task TestOneCallHistorical()
  {
    var client = new OpenWeatherMapClient(ApiKey);
    var ts = DateTimeOffset.FromUnixTimeSeconds(1672527600); // 2023-01-01 00:00:00
    var weather = await client.OneCall.QueryHistoricalAsync("Linz,AT", ts);
    Assert.NotNull(weather);
    Assert.Equal(48.3059, weather.Latitude, 3);
    Assert.Equal(14.2862, weather.Longitude, 3);
    Assert.Equal("Europe/Vienna", weather.TimeZone);
    Assert.Equal(ts, weather.TimeStamp);

    weather = await client.OneCall.GetHistoricalByCoordinatesAsync(48.3059, 14.2862, ts);
    Assert.NotNull(weather);
    Assert.Equal(48.3059, weather.Latitude, 3);
    Assert.Equal(14.2862, weather.Longitude, 3);
    Assert.Equal("Europe/Vienna", weather.TimeZone);
    Assert.Equal(ts, weather.TimeStamp);
  }
}