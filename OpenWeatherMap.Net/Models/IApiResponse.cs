using System.Net;
using OpenWeatherMap.Net.Exceptions;

namespace OpenWeatherMap.Net.Models;

public interface IApiResponse<out T> where T : class
{
  /// <summary>
  /// HTTP response status code.
  /// </summary>
  HttpStatusCode StatusCode { get; }

  /// <summary>
  /// The reason phrase which typically is sent by the server together with the status code.
  /// </summary>
  string? ReasonPhrase { get; }

  /// <summary>
  /// Indicates whether the request was successful.
  /// </summary>
  bool IsSuccess { get; }

  /// <summary>
  /// The response content.
  /// </summary>
  T? Content { get; }

  /// <summary>
  /// Does the response have content?
  /// </summary>
  bool HasContent { get; }

  /// <summary>
  /// The <see cref="OpenWeatherMapException"/> in case of an unsuccessful request.
  /// </summary>
  OpenWeatherMapException? Error { get; }
}