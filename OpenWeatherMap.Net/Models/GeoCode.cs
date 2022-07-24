namespace OpenWeatherMap.Net.Models;

public sealed class GeoCode
{
  public string Name { get; internal set; } = null!;
  public IDictionary<string, string> LocalNames { get; internal set; } = null!;
  public double Latitude { get; internal set; }
  public double Longitude { get; internal set; }
  public string Country { get; internal set; } = null!;
}