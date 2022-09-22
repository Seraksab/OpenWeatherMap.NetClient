using System.Globalization;

namespace OpenWeatherMap.NetClient.Models;

/// <summary>
/// Defines various options for the <see cref="OpenWeatherMapClient"/>
/// </summary>
public sealed class OpenWeatherMapOptions
{
  /// <summary>
  /// Creates an instance of <see cref="OpenWeatherMapOptions"/> with the default options
  /// </summary>
  public OpenWeatherMapOptions()
  {
  }

  /// <summary>
  /// Language to get textual outputs in
  /// </summary>
  public CultureInfo Culture { get; set; } = new("en");

  /// <summary>
  /// Duration the responses will be cached
  /// </summary>
  public TimeSpan CacheDuration { get; set; } = TimeSpan.Zero;

  /// <summary>
  /// How often to retry on timeout or API error
  /// </summary>
  public int RetryCount { get; set; } = 1;

  /// <summary>
  /// Duration to wait between retries
  /// </summary>
  public Func<int, TimeSpan> RetryWaitDurationProvider { get; set; } = _ => TimeSpan.Zero;
}