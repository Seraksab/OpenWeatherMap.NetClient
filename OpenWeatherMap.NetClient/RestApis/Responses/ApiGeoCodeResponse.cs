using System.Text.Json.Serialization;

namespace OpenWeatherMap.NetClient.RestApis.Responses;

internal sealed class ApiGeoCodeResponse
{
  [JsonPropertyName("name")] public string Name { get; set; } = null!;
  [JsonPropertyName("local_names")] public IDictionary<string, string> LocalNames { get; set; } = null!;
  [JsonPropertyName("lat")] public double Latitude { get; set; }
  [JsonPropertyName("lon")] public double Longitude { get; set; }
  [JsonPropertyName("country")] public string Country { get; set; } = null!;
}