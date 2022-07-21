using System.Net;

namespace OpenWeatherMap.Net.UnitTests;

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
    var response = await client.CurrentWeatherByName("Linz,AT");
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
  }

  [Fact]
  public async Task TestCityNameNotFound()
  {
    var client = new OpenWeatherMapClient(ApiKey);
    var result = await client.CurrentWeatherByName("foobar");
    Assert.Null(result.Content);
  }

  [Fact]
  public async Task TestCityName()
  {
    var client = new OpenWeatherMapClient(ApiKey);
    var result = await client.CurrentWeatherByName("Linz,AT");
    Assert.NotNull(result.Content);
    Assert.Equal("Linz", result.Content?.CityName);
    Assert.Equal("AT", result.Content?.Country);

    var cachedResult = await client.CurrentWeatherByName("Linz,AT");
    Assert.True(result == cachedResult);
  }

  [Fact]
  public async Task TestNullCityName()
  {
    var client = new OpenWeatherMapClient(ApiKey);
    await Assert.ThrowsAsync<ArgumentNullException>(() => client.CurrentWeatherByName(null));
  }

  [Fact]
  public async Task TestNullZipCode()
  {
    var client = new OpenWeatherMapClient(ApiKey);
    await Assert.ThrowsAsync<ArgumentNullException>(() => client.CurrentWeatherByZip(null));
  }

  [Fact]
  public async Task TestZipCode()
  {
    var client = new OpenWeatherMapClient(ApiKey);
    var result = await client.CurrentWeatherByZip("4020,AT");
    Assert.NotNull(result.Content);
    Assert.Equal("Linz", result.Content?.CityName);
    Assert.Equal("AT", result.Content?.Country);

    var cachedResult = await client.CurrentWeatherByZip("4020,AT");
    Assert.True(result == cachedResult);
  }

  [Fact]
  public async Task TestCoordinates()
  {
    var client = new OpenWeatherMapClient(ApiKey);
    var result = await client.CurrentWeatherByCoordinates(48.3059D, 14.2862D);
    Assert.NotNull(result.Content);
    Assert.Equal("Linz", result.Content?.CityName);
    Assert.Equal("AT", result.Content?.Country);

    var cachedResult = await client.CurrentWeatherByCoordinates(48.3059D, 14.2862D);
    Assert.True(result == cachedResult);
  }
}