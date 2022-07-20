using System.Net;
using OpenWeatherMap.Net.Models;

namespace OpenWeatherMap.Net.UnitTests;

public class Tests
{
  private const string ApiKey = "[API-KEY]";

  [Fact]
  public async Task TestInvalidApiKey()
  {
    // illegal api key format
    Assert.Throws<ArgumentException>(() => new OpenWeatherMap("foo"));

    // invalid api key => unauthorized
    var client = new OpenWeatherMap("00000000000000000000000000000000");
    var exception = await Assert.ThrowsAsync<OpenWeatherMapException>(() => client.CurrentWeatherByName("Linz,AT"));
    Assert.Equal(HttpStatusCode.Unauthorized, exception.StatusCode);
  }

  [Fact]
  public async Task TestCityNameNotFound()
  {
    var client = new OpenWeatherMap(ApiKey);
    var result = await client.CurrentWeatherByName("foobar");
    Assert.Null(result);
  }

  [Fact]
  public async Task TestCityName()
  {
    var client = new OpenWeatherMap(ApiKey);
    var result = await client.CurrentWeatherByName("Linz,AT");
    Assert.NotNull(result);
    Assert.Equal("Linz", result?.CityName);
    Assert.Equal("AT", result?.Country);

    var cachedResult = await client.CurrentWeatherByName("Linz,AT");
    Assert.True(result == cachedResult);
  }

  [Fact]
  public async Task TestNullCityName()
  {
    var client = new OpenWeatherMap(ApiKey);
    await Assert.ThrowsAsync<ArgumentNullException>(() => client.CurrentWeatherByName(null));
  }

  [Fact]
  public async Task TestNullZipCode()
  {
    var client = new OpenWeatherMap(ApiKey);
    await Assert.ThrowsAsync<ArgumentNullException>(() => client.CurrentWeatherByZip(null));
  }

  [Fact]
  public async Task TestZipCode()
  {
    var client = new OpenWeatherMap(ApiKey);
    var result = await client.CurrentWeatherByZip("4020,AT");
    Assert.Equal("Linz", result?.CityName);
    Assert.Equal("AT", result?.Country);

    var cachedResult = await client.CurrentWeatherByZip("4020,AT");
    Assert.True(result == cachedResult);
  }

  [Fact]
  public async Task TestCoordinates()
  {
    var client = new OpenWeatherMap(ApiKey);
    var result = await client.CurrentWeatherByCoordinates(48.3059D, 14.2862D);
    Assert.Equal("Linz", result?.CityName);
    Assert.Equal("AT", result?.Country);

    var cachedResult = await client.CurrentWeatherByCoordinates(48.3059D, 14.2862D);
    Assert.True(result == cachedResult);
  }
}