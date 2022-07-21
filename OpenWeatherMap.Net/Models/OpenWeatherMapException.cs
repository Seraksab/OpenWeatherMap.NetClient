using System.Net;

namespace OpenWeatherMap.Net.Models;

public sealed class OpenWeatherMapException : Exception
{
  /// <summary>
  /// HTTP response status code.
  /// </summary>
  public HttpStatusCode StatusCode { get; }

  /// <summary>
  /// The reason phrase which typically is sent by the server together with the status code.
  /// </summary>
  public string? ReasonPhrase { get; }

  internal OpenWeatherMapException(HttpStatusCode statusCode, string? reasonPhrase, Exception? inner)
    : base($"Error on API request", inner)
  {
    StatusCode = statusCode;
    ReasonPhrase = reasonPhrase;
  }
}