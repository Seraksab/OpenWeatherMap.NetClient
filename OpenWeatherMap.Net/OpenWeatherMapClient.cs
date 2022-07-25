﻿using System.Text.RegularExpressions;
using OpenWeatherMap.Net.Apis;
using OpenWeatherMap.Net.Apis.Impl;
using OpenWeatherMap.Net.Models;

namespace OpenWeatherMap.Net;

public sealed class OpenWeatherMapClient : IOpenWeatherMapClient
{
  private static readonly Regex ApiKeyRegex = new(@"^[0-9a-f]{32}$");

  private readonly Lazy<IGeocodingApi> _geoCoding;
  private readonly Lazy<IAirPollutionApi> _airPollution;
  private readonly Lazy<ICurrentWeatherApi> _currentWeather;

  public OpenWeatherMapClient(string apiKey, IOpenWeatherMapOptions? options = null)
  {
    if (apiKey == null) throw new ArgumentNullException(nameof(apiKey));
    if (!ApiKeyRegex.IsMatch(apiKey)) throw new ArgumentException($"'{apiKey}' is not a valid API key");

    _geoCoding = new Lazy<IGeocodingApi>(() => new GeocodingApiImpl(apiKey, options));
    _airPollution = new Lazy<IAirPollutionApi>(() => new AirPollutionApiImpl(apiKey, options));
    _currentWeather = new Lazy<ICurrentWeatherApi>(() => new CurrentWeatherApiImpl(apiKey, options));
  }

  public ICurrentWeatherApi CurrentWeather => _currentWeather.Value;

  public IGeocodingApi Geocoding => _geoCoding.Value;

  public IAirPollutionApi AirPollution => _airPollution.Value;
}