# ![](./logo32.png) OpenWeatherMap.NetClient

[![Nuget](https://img.shields.io/nuget/v/OpenWeatherMap.NetClient?style=flat-square)](https://www.nuget.org/packages/OpenWeatherMap.NetClient)
[![Nuget](https://img.shields.io/nuget/dt/OpenWeatherMap.NetClient?style=flat-square)](https://www.nuget.org/packages/OpenWeatherMap.NetClient)
![GitHub](https://img.shields.io/github/license/Seraksab/OpenWeatherMap.NetClient)

A simple, asynchronous .NET client to fetch weather information from
the [OpenWeatherMap](https://openweathermap.org/) APIs.

Support for response caching and retries is built in.  
Numerical values are parsed to and returned as units from [Units.NET](https://github.com/angularsen/UnitsNet)
(where applicable) to ease the conversion between different measurement systems and avoid unit confusion.

## Supported APIs

* [Current Weather](https://openweathermap.org/current)  
  Current weather data for any location on Earth including over 200,000 cities.
* [Geocoding](https://openweathermap.org/api/geocoding-api)  
  Tool to ease the search for locations while working with geographic names and coordinates.
* [Air Pollution](https://openweathermap.org/api/air-pollution)  
  Provides current, forecast and historical air pollution data for any coordinates on the globe.
* [Basic weather maps](https://openweathermap.org/api/weathermaps)  
  Provides many kinds of weather maps including Precipitation, Clouds, Pressure, Temperature, Wind.
* [3-hour Forecast 5 days](https://openweathermap.org/forecast5)  
  Provides 5 days weather forecast data with 3-hour steps.
* [One Call 3.0](https://openweathermap.org/api/one-call-3)  
  Contains 3 endpoints and provides access to various data:
    * Current weather, minute forecast for 1 hour, hourly forecast for 48 hours, daily forecast for 8 days
    * Historical weather data for any timestamp from 1st January 1979 till now
    * Aggregated historical weather data for a particular date from 2nd January 1979

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

## Exception Handling

This client uses [Refit](https://github.com/reactiveui/refit) to handle the actual HTTP requests.
Exceptions thrown by Refit are not caught, wrapped or altered in any form.

```csharp
// ...
try
{
  var weather = await client.CurrentWeather.QueryAsync("Linz,AT");
}
catch (ApiException exception)
{
  //exception handling
}
```

For more details refer to
the [Refit documentation on handling exceptions](https://github.com/reactiveui/refit#handling-exceptions).

## Configuration

| Setting                   | Default value            |                                            |
|---------------------------|--------------------------|--------------------------------------------|
| Culture                   | ```"en"```               | Language to get textual outputs in         |
| CacheDuration             | ```TimeSpan.Zero ```     | Duration the API responses will be cached  |
| RetryCount                | ```1```                  | How often to retry on timeout or API error |
| RetryWaitDurationProvider | ```_ => TimeSpan.Zero``` | Duration to wait between retries           |

You can modify the default configuration of the client by additionally passing _OpenWeatherMapOptions_

```csharp
var client = new OpenWeatherMapClient("[API_KEY]", new OpenWeatherMapOptions
{
  Culture = new CultureInfo("en"),
  CacheDuration = TimeSpan.FromMinutes(10),
  RetryCount = 3,
  RetryWaitDurationProvider = attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)) // exponential back-off
});
```

### HttpClient

Each individual API exposes the `HttpClient` instance being used should you need or want to configure it.

```csharp
var client = new OpenWeatherMapClient("[API_KEY]");
var currentWeatherApi = client.CurrentWeather;

// configure HttpClient
currentWeatherApi.Client.Timeout = TimeSpan.FromSeconds(5);
// ...
```

### ASP.NET

Simply call the provided extension method to register the service in your Startup.cs

```csharp
// with default configuration
services.AddOpenWeatherMap("[API KEY]");

// or with custom client configuration
services.AddOpenWeatherMap("[API KEY]", new OpenWeatherMapOptions
{
  CacheDuration = TimeSpan.FromMinutes(2)
});
```

## Dependencies

* [Refit](https://github.com/reactiveui/refit) - REST request handling
* [Polly](https://github.com/App-vNext/Polly) - retries and caching
* [Units.NET](https://github.com/angularsen/UnitsNet) - units for numerical weather values
