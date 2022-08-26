# ![](./logo32.png) OpenWeatherMap.NetClient

[![Nuget](https://img.shields.io/nuget/v/OpenWeatherMap.NetClient?style=flat-square)](https://www.nuget.org/packages/OpenWeatherMap.NetClient)
[![Nuget](https://img.shields.io/nuget/dt/OpenWeatherMap.NetClient?style=flat-square)](https://www.nuget.org/packages/OpenWeatherMap.NetClient)

A simple, asynchronous .NET client to fetch weather information from
the [OpenWeatherMap](https://openweathermap.org/) APIs.

Support for response caching is built in.  
Numerical values are parsed to and returned as units from [Units.NET](https://github.com/angularsen/UnitsNet)
(where applicable) to ease the conversion between different measurement systems and avoid unit confusion.

**Supported APIs:**

* [Current Weather](https://openweathermap.org/current) - Current weather data for any location on Earth including over
  200,000 cities.
* [Geocoding](https://openweathermap.org/api/geocoding-api) - Tool to ease the search for locations while working with
  geographic names and coordinates.
* [Air Pollution](https://openweathermap.org/api/air-pollution) - Provides current, forecast and historical air
  pollution data for any coordinates on the globe.
* [Basic weather maps](https://openweathermap.org/api/weathermaps) - Provides many kinds of weather maps including
  Precipitation, Clouds, Pressure, Temperature, Wind.
* [3-hour Forecast 5 days](https://openweathermap.org/forecast5) - Provides 5 days weather forecast data with 3-hour
  steps.

## Installation

OpenWeatherMap.NetClient is available from [NuGet](https://www.nuget.org/packages/OpenWeatherMap.NetClient)

## Usage

Creating a client is as simple as just passing in your Api Key to the constructor.

```csharp
var client = new OpenWeatherMapClient("[API_KEY]");

// querying the current weather for a location based on name
var weather = await client.CurrentWeather.QueryAsync("Linz,AT");
Console.Out.WriteLine($"The current weather in Linz is '{weather.WeatherDescription}' " +
                      $"at {weather.Temperature.DegreesCelsius}°C");

// querying the current air pollution for geographical coordinates
var airPollution = await client.AirPollution.QueryCurrentAsync(48.3059, 14.2862);
Console.Out.WriteLine($"The current air quality index is '{airPollution.AirQualityIndex.ToString()}'");

// getting the global temperature map (zoom 0, tile 0/0)
var map = await client.BasicWeatherMaps.GetMapAsync(BasicWeatherMapLayer.Temperature, 0, 0, 0);
await File.WriteAllBytesAsync("C:\\ [...] \\temperature_map.png", map);

// querying the next 2 segments of the weather forecast
var response = await client.Forecast5Days.QueryAsync("Vienna, AT", 2);
var forecast = response.Forecast.ToArray();
Console.Out.WriteLine($"At {forecast[1].ForecastTimeStamp.ToShortTimeString()} " +
                      $"the weather in Vienna will be '{forecast[1].WeatherCondition}'");
```

### Client configuration

Caching is disabled by default.  
The default cache duration is set to 10 minutes (if cache is enabled).  
The default culture is set to 'en'.

You can modify the default configuration of the client by additionally passing _IOpenWeatherMapOptions_

```csharp
var client = new OpenWeatherMapClient("[API_KEY]", new OpenWeatherMapOptions
{
  Culture = new CultureInfo("en"),
  CacheEnabled = true,
  CacheDuration = TimeSpan.FromMinutes(10)
});
```

### ASP.NET

Simply call the provided extension method to register the service in your Startup.cs

```csharp
// with default configuration
services.AddOpenWeatherMap("[API KEY]");

// or with custom client configuration
services.AddOpenWeatherMap("[API KEY]", new OpenWeatherMapOptions
{
  CacheEnabled = true,
  CacheDuration = TimeSpan.FromMinutes(5)
});
```

## Dependencies

* [Refit](https://github.com/reactiveui/refit) - REST request handling
* [LazyCache](https://github.com/alastairtree/LazyCache) - caching support
* [Units.NET](https://github.com/angularsen/UnitsNet) - units for numerical weather values
