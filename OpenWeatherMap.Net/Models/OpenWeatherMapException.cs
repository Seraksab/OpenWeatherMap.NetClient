using System.Net;
using Refit;

namespace OpenWeatherMap.Net.Models;

public sealed class OpenWeatherMapException : Exception
{
  public HttpStatusCode? StatusCode { get; }
  public string? ReasonPhrase { get; }

  internal OpenWeatherMapException(string? message) : base(message)
  {
  }

  internal OpenWeatherMapException(IApiResponse response)
    : base($"Error on API call. Reason: {response.ReasonPhrase}", response.Error)
  {
    StatusCode = response.StatusCode;
    ReasonPhrase = response.ReasonPhrase;
  }
}