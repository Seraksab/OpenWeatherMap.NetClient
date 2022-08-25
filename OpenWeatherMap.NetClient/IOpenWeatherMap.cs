using OpenWeatherMap.NetClient.Apis;

namespace OpenWeatherMap.NetClient;

/// <summary>
/// Access to different OpenWeatherMap APIs
/// </summary>
public interface IOpenWeatherMap
{
  /// <summary>
  /// Access to the 'Current Weather Data' API.
  /// Provides current weather data for any location on Earth including over 200,000 cities.
  /// Data is collected and processed from different sources such as global and local weather models, satellites, radars and
  /// a vast network of weather stations.
  /// </summary>
  ICurrentWeatherApi CurrentWeather { get; }

  /// <summary>
  /// Access to the 'Geocoding' API.
  /// A simple tool developed to ease the search for locations while working with geographic names and coordinates.
  /// </summary>
  IGeocodingApi Geocoding { get; }

  /// <summary>
  /// Access to the 'Air Pollution' API.
  /// Provides current, forecast and historical air pollution data for any coordinates on the globe.
  /// </summary>
  IAirPollutionApi AirPollution { get; }

  /// <summary>
  /// Access to 'Basic weather maps' API
  /// Provides many kinds of weather maps including Precipitation, Clouds, Pressure, Temperature, Wind.
  /// </summary>
  /// <remarks>
  /// Caching NOT supported
  /// </remarks>
  IBasicWeatherMapsApi BasicWeatherMaps { get; }

  /// <summary>
  /// Access to the '3-hour Forecast 5 days' API.
  /// The 5 day forecast includes weather data with 3-hour steps.
  /// </summary>
  IForecast5DaysApi Forecast5Days { get; }
}