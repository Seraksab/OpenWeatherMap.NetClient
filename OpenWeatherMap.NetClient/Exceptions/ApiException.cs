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

  /// <summary>
  /// The HTTP response content as string.
  /// </summary>
  public string? Content { get; }

  /// <summary>
  /// Whether the response has content or not 
  /// </summary>
  public bool HasContent => !string.IsNullOrWhiteSpace(Content);

  internal ApiException(HttpStatusCode statusCode, string? reasonPhrase, string? content)
    : base($"API request failed with status code: {(int)statusCode} ({reasonPhrase})")
  {
    StatusCode = statusCode;
    ReasonPhrase = reasonPhrase;
    Content = content;
  }
}