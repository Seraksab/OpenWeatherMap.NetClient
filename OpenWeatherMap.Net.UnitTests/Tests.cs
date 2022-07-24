using System.Net;

namespace OpenWeatherMap.Net.UnitTests;

public class Tests
{
  // private const string ApiKey = "[API-KEY]";
  private const string ApiKey = "e9e6e24f16a94f0353ad8b4ddcde8123";

  [Fact]
  public async Task TestInvalidApiKey()
  {
    // illegal api key format
    Assert.Throws<ArgumentException>(() => new OpenWeatherMapClient("foo"));

    // invalid api key => unauthorized
    var client = new OpenWeatherMapClient("00000000000000000000000000000000");
    var response = await client.QueryWeatherAsync("Linz,AT");
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
  }

  [Fact]
  public async Task TestCityNameNotFound()
  {
    var client = new OpenWeatherMapClient(ApiKey);
    var result = await client.QueryWeatherAsync("foobar");
    Assert.Null(result.Content);
  }

  [Fact]
  public async Task TestCityName()
  {
    var client = new OpenWeatherMapClient(ApiKey);
    var result = await client.QueryWeatherAsync("Linz,AT");
    Assert.NotNull(result.Content);
    Assert.Equal("Linz", result.Content?.CityName);
    Assert.Equal("AT", result.Content?.Country);

    var cachedResult = await client.QueryWeatherAsync("Linz,AT");
    Assert.True(result == cachedResult);
  }

  [Fact]
  public async Task TestNullCityName()
  {
    var client = new OpenWeatherMapClient(ApiKey);
    await Assert.ThrowsAsync<ArgumentNullException>(() => client.QueryWeatherAsync(null));
  }

  [Fact]
  public async Task TestCoordinates()
  {
    var client = new OpenWeatherMapClient(ApiKey);
    var result = await client.QueryWeatherAsync(48.3059D, 14.2862D);
    Assert.NotNull(result.Content);
    Assert.Equal("Linz", result.Content?.CityName);
    Assert.Equal("AT", result.Content?.Country);

    var cachedResult = await client.QueryWeatherAsync(48.3059D, 14.2862D);
    Assert.True(result == cachedResult);
  }

  [Fact]
  public async Task TestGeoCode()
  {
    var client = new OpenWeatherMapClient(ApiKey);
    var result = await client.QueryGeoCodeAsync("Linz,AT");
    Assert.NotNull(result.Content);
    Assert.NotEmpty(result.Content!);
    Assert.Equal("AT", result.Content!.First().Country);
    Assert.Equal("Linz", result.Content!.First().Name);
    Assert.Equal(48.3059, result.Content!.First().Latitude, 3);
    Assert.Equal(14.2862, result.Content!.First().Longitude, 3);
  }
}