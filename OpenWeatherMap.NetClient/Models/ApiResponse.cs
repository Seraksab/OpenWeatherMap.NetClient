using System.Net;
using OpenWeatherMap.NetClient.Exceptions;

namespace OpenWeatherMap.NetClient.Models;

public sealed class ApiResponse<T> : IApiResponse<T> where T : class
{
  internal ApiResponse(HttpStatusCode statusCode, string? reasonPhrase, T? content, Exception? error)
  {
    StatusCode = statusCode;
    ReasonPhrase = reasonPhrase;
    Content = content;
    if (!IsSuccess)
    {
      Error = new OpenWeatherMapException(statusCode, reasonPhrase, error);
    }
  }

  public HttpStatusCode StatusCode { get; }
  public string? ReasonPhrase { get; }
  public bool IsSuccess => (int)StatusCode >= 200 && (int)StatusCode <= 299;
  public T? Content { get; }
  public bool HasContent => Content != null;
  public OpenWeatherMapException? Error { get; }
}