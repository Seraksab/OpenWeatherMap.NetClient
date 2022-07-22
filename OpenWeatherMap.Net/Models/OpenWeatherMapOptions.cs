using System.Globalization;

namespace OpenWeatherMap.Net.Models;

public sealed class OpenWeatherMapOptions : IOpenWeatherMapOptions
{
  public static class Defaults
  {
    public static readonly CultureInfo Culture = new("en");
    public static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10);
    public static readonly bool CacheEnabled = true;
  }

  public CultureInfo? Culture { get; set; }
  public bool? CacheEnabled { get; set; }
  public TimeSpan? CacheDuration { get; set; }
}