using System.Net;
using OpenWeatherMap.NetClient.Exceptions;

namespace OpenWeatherMap.NetClient.Models;

/// <summary>
/// Implementation of <see cref="IApiResponse{T}"/>
/// </summary>
/// <typeparam name="T">Type of the mapped response content</typeparam>
public sealed class ApiResponse<T> : IApiResponse<T> where T : class
{
  internal ApiResponse(HttpStatusCode statusCode, string? reasonPhrase, T? content, Exception? error)
  {
    StatusCode = statusCode;
    ReasonPhrase = reasonPhrase;
    Content = content;
    if (error != null)
    {
      Error = new OpenWeatherMapException(statusCode, reasonPhrase, error);
    }
  }

  /// <inheritdoc />
  public HttpStatusCode StatusCode { get; }

  /// <inheritdoc />
  public string? ReasonPhrase { get; }

  /// <inheritdoc />
  public bool IsSuccess => (int)StatusCode >= 200 && (int)StatusCode <= 299 && Error == null;

  /// <inheritdoc />
  public T? Content { get; }

  /// <inheritdoc />
  public bool HasContent => Content != null;

  /// <inheritdoc />
  public OpenWeatherMapException? Error { get; }
}