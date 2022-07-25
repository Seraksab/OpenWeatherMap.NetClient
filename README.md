# OpenWeatherMap.NetClient

A simple, asynchronous .NET client to fetch weather information from
the [OpenWeatherMap](https://openweathermap.org/) APIs

Currently supported APIs:

* [Current Weather](https://openweathermap.org/current) - Current weather data for any location on Earth including over
  200,000 cities
* [Geocoding](https://openweathermap.org/api/geocoding-api) - Tool to ease the search for locations while working with
  geographic names and coordinates
* [Air Pollution](https://openweathermap.org/api/air-pollution) - Provides current, forecast and historical air
  pollution data for any coordinates on the globe

## Installation

This is still work in progress. Once finished, the client will be published on NuGet.

TODO

## Usage

Creating a client is as simple as just passing in your Api Key to the constructor.

```csharp
var client = new OpenWeatherMap("[API_KEY]");

// querying the current weather for a location based on name
var weatherResponse = await client.CurrentWeather.QueryAsync("Linz,AT");
if (weatherResponse.IsSuccess)
{
  var content = weatherResponse.Content;
  Console.Out.WriteLine($"The current weather in Linz is '{content.WeatherDescription}' " +
                        $"at {content.Temperature.DegreesCelsius}°C");
}

// querying the current air pollution for geographical coordinates
var airPollutionResponse = await client.AirPollution.QueryCurrentAsync(48.3059, 14.2862);
if (airPollutionResponse.IsSuccess)
{
  var aqi = airPollutionResponse.Content.AirQualityIndex;
  Console.Out.WriteLine($"The current air quality index is '{aqi.ToString()}'");
}
```

### Client configuration

You can modify the default configuration of the client by additionally passing _IOpenWeatherMapOptions_

```csharp
var client = new OpenWeatherMapClient("[API_KEY]", new OpenWeatherMapOptions
{
  Culture = new CultureInfo("en"),
  CacheEnabled = true,
  CacheDuration = TimeSpan.FromMinutes(10)
});
```

## Dependencies

* [Refit](https://github.com/reactiveui/refit) - used to handle REST requests
* [LazyCache](https://github.com/alastairtree/LazyCache) - caching support
* [Units.NET](https://github.com/angularsen/UnitsNet) - where applicable, numerical values are parsed to and returned as
  units from this library to ease the conversion between different measurement systems and avoid unit confusion
