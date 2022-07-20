using System.Globalization;

namespace OpenWeatherMap.Net.Models;

public class OpenWeatherMapOptions
{
  internal static readonly CultureInfo DefaultCulture = new("en");
  internal static readonly TimeSpan DefaultCacheDuration = TimeSpan.FromMinutes(10);

  /// <summary>
  /// Language to get textual outputs in
  /// </summary>
  public CultureInfo? Culture { get; set; } = DefaultCulture;

  /// <summary>
  /// Duration the API responses will be cached
  /// </summary>
  public TimeSpan? CacheDuration { get; set; } = DefaultCacheDuration;
}