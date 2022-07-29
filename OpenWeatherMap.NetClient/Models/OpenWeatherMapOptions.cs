using System.Globalization;

namespace OpenWeatherMap.NetClient.Models;

/// <summary>
/// Implementation of <see cref="IOpenWeatherMapOptions"/>
/// </summary>
public sealed class OpenWeatherMapOptions : IOpenWeatherMapOptions
{
  /// <summary>
  /// The default options that apply if no options or no value is set
  /// </summary>
  public static class Defaults
  {
    /// <summary>
    /// The default culture
    /// </summary>
    public static readonly CultureInfo Culture = new("en");

    /// <summary>
    /// The default cache duration
    /// </summary>
    public static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10);

    /// <summary>
    /// Whether caching is enabled by default or not
    /// </summary>
    public static readonly bool CacheEnabled = false;
  }

  /// <inheritdoc />
  public CultureInfo? Culture { get; set; }

  /// <inheritdoc />
  public bool? CacheEnabled { get; set; }

  /// <inheritdoc />
  public TimeSpan? CacheDuration { get; set; }
}