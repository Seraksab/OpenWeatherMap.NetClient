using System.Globalization;

namespace OpenWeatherMap.NetClient.Models;

/// <summary>
/// OpenWeatherMap client configuration
/// </summary>
public interface IOpenWeatherMapOptions
{
  /// <summary>
  /// Language to get textual outputs in
  /// </summary>
  public CultureInfo? Culture { get; }

  /// <summary>
  /// Whether to cache the API responses
  /// </summary>
  public bool? CacheEnabled { get; }

  /// <summary>
  /// Duration the API responses will be cached if the cache is enabled
  /// </summary>
  public TimeSpan? CacheDuration { get; }
}