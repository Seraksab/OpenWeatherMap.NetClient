# OpenWeatherMap.Net

A simple, asynchronous .NET client to fetch current weather information from
the [OpenWeatherMap](https://openweathermap.org/) API

This client utilizes [Refit](https://github.com/reactiveui/refit) to handle the REST requests
and [LazyCache](https://github.com/alastairtree/LazyCache) for caching support.  
Where applicable, numerical values are parsed to and returned as units
from [Units.NET](https://github.com/angularsen/UnitsNet) to ease the conversion between different measurement systems
and avoid unit confusion.


## Installation

TODO

## Usage

Creating a client is as simple as just passing in your Api Key.  
You can then fetch weather information by:

* **Name**: City name, state code (only for the US) and country code divided by comma (ISO 3166 country codes)
* **Zip**: Zip/post code and country code divided by comma (ISO 3166 country codes)
* **Coordinates**: Geographical coordinates (latitude, longitude)

```csharp
var client = new OpenWeatherMapClient("[API_KEY]");
var result = await client.CurrentWeatherByName("Linz,AT");

// or alternatively:
// var result = await client.CurrentWeatherByZip("4020,AT");
// var result = await client.CurrentWeatherByCoordinates(48.3059, 14.2862);

if (result.IsSuccess)
{
  var content = result.Content;
  Console.Out.WriteLine($"The current weather in Linz is '{content?.WeatherCondition}' " +
                        $"at {content?.Temperature.DegreesCelsius}°C");
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
