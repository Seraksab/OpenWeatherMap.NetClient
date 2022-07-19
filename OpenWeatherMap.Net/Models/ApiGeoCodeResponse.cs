using System.Text.Json.Serialization;

namespace OpenWeatherMap.Net.Models;

public class ApiGeoCodeResponse
{
  [JsonPropertyName("name")] public string Name { get; set; } = null!;
  [JsonPropertyName("lat")] public double Latitude { get; set; }
  [JsonPropertyName("lon")] public double Longitude { get; set; }
  [JsonPropertyName("country")] public string Country { get; set; } = null!;
}