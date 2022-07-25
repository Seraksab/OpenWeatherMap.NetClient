using OpenWeatherMap.NetClient.Apis;

namespace OpenWeatherMap.NetClient;

public interface IOpenWeatherMap
{
  /// <summary>
  /// API to fetch current weather data for any location on Earth including over 200,000 cities. 
  /// </summary>
  /// <returns>An implementation of <see cref="ICurrentWeatherApi"/></returns>
  ICurrentWeatherApi CurrentWeather { get; }

  /// <summary>
  /// The Geocoding API is a tool to ease the search for locations while working with geographic names and coordinates.
  /// </summary>
  /// <returns>An implementation of <see cref="IGeocodingApi"/></returns>
  IGeocodingApi Geocoding { get; }
  
  /// <summary>
  /// The Air Pollution API provides current, forecast and historical air pollution data for any coordinates on the globe.
  /// </summary>
  /// <returns>An implementation of <see cref="IAirPollutionApi"/></returns>
  IAirPollutionApi AirPollution { get; }
}