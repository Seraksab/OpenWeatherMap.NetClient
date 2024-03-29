﻿using System.Text.RegularExpressions;
using OpenWeatherMap.NetClient.Apis;
using OpenWeatherMap.NetClient.Models;

namespace OpenWeatherMap.NetClient;

/// <summary>
/// Client providing access to the OpenWeatherMap APIs. Implements <see cref="IOpenWeatherMap"/>
/// </summary>
public sealed class OpenWeatherMapClient : IOpenWeatherMap
{
  private static readonly Regex ApiKeyRegex = new(@"^[0-9a-f]{32}$");

  private readonly Lazy<IGeocodingApi> _geoCoding;
  private readonly Lazy<IAirPollutionApi> _airPollution;
  private readonly Lazy<ICurrentWeatherApi> _currentWeather;
  private readonly Lazy<IBasicWeatherMapsApi> _basicWeatherMaps;
  private readonly Lazy<IForecast5DaysApi> _forecast5Days;
  private readonly Lazy<IOneCallApi> _oneCall;

  /// <summary>
  /// Creates a new OpenWeatherMap instance
  /// </summary>
  /// <returns>An implementation of <see cref="IOpenWeatherMap"/></returns>
  /// <param name="apiKey">Your unique API key</param>
  /// <param name="options">Optional client configuration</param>
  public OpenWeatherMapClient(string apiKey, OpenWeatherMapOptions? options = null)
  {
    if (apiKey == null) throw new ArgumentNullException(nameof(apiKey));
    if (!ApiKeyRegex.IsMatch(apiKey)) throw new ArgumentException($"'{apiKey}' is not a valid API key");

    options ??= new OpenWeatherMapOptions();

    _geoCoding = new Lazy<IGeocodingApi>(() => new GeocodingApi(apiKey, options));
    _airPollution = new Lazy<IAirPollutionApi>(() => new AirPollutionApi(apiKey, options));
    _currentWeather = new Lazy<ICurrentWeatherApi>(() => new CurrentWeatherApi(apiKey, options));
    _basicWeatherMaps = new Lazy<IBasicWeatherMapsApi>(() => new BasicWeatherMapsApi(apiKey, options));
    _forecast5Days = new Lazy<IForecast5DaysApi>(() => new Forecast5DaysApi(apiKey, options));
    _oneCall = new Lazy<IOneCallApi>(() => new OneCallApi(apiKey, options));
  }

  /// <inheritdoc />
  public ICurrentWeatherApi CurrentWeather => _currentWeather.Value;

  /// <inheritdoc />
  public IGeocodingApi Geocoding => _geoCoding.Value;

  /// <inheritdoc />
  public IAirPollutionApi AirPollution => _airPollution.Value;

  /// <inheritdoc />
  public IBasicWeatherMapsApi BasicWeatherMaps => _basicWeatherMaps.Value;

  /// <inheritdoc />
  public IForecast5DaysApi Forecast5Days => _forecast5Days.Value;

  /// <inheritdoc />
  public IOneCallApi OneCall => _oneCall.Value;
}