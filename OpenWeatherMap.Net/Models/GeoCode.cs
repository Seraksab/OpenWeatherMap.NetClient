namespace OpenWeatherMap.Net.Models;

public sealed class GeoCode
{
  /// <summary>
  /// Name of the found location
  /// </summary>
  public string Name { get; internal set; } = null!;

  /// <summary>
  /// Name of the found location in differen tlanguages. The list of names can be different for different locations
  /// </summary>
  public IDictionary<string, string> LocalNames { get; internal set; } = null!;

  /// <summary>
  /// Geographical coordinates of the found location (latitude)
  /// </summary>
  public double Latitude { get; internal set; }

  /// <summary>
  /// Geographical coordinates of the found location (longitude)
  /// </summary>
  public double Longitude { get; internal set; }

  /// <summary>
  /// Country of the found location
  /// </summary>
  public string Country { get; internal set; } = null!;
}