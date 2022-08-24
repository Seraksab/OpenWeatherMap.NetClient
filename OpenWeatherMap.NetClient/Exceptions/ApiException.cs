using System.Net;

namespace OpenWeatherMap.NetClient.Exceptions;

/// <summary>
/// Error that occured while sending an API request
/// </summary>
public sealed class ApiException : Exception
{
  /// <summary>
  /// HTTP response status code.
  /// </summary>
  public HttpStatusCode StatusCode { get; }

  /// <summary>
  /// The reason phrase which typically is sent by the server together with the status code.
  /// </summary>
  public string? ReasonPhrase { get; }

  internal ApiException(HttpStatusCode statusCode, string? reasonPhrase, Exception? inner)
    : base("Error on API request", inner)
  {
    StatusCode = statusCode;
    ReasonPhrase = reasonPhrase;
  }
}