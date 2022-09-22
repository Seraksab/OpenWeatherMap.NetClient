using LazyCache;
using OpenWeatherMap.NetClient.Models;
using Refit;
using ApiException = OpenWeatherMap.NetClient.Exceptions.ApiException;

namespace OpenWeatherMap.NetClient.RestApis.Clients;

internal sealed class RestClient<T> where T : class
{
  private readonly T _api;

  private readonly bool _cacheEnabled;
  private readonly TimeSpan _cacheDuration;
  private readonly Lazy<IAppCache> _cache;

  internal RestClient(string url, IOpenWeatherMapOptions? options = null)
  {
    _cacheEnabled = options?.CacheEnabled ?? OpenWeatherMapOptions.Defaults.CacheEnabled;
    _cacheDuration = options?.CacheDuration ?? OpenWeatherMapOptions.Defaults.CacheDuration;

    _cache = new Lazy<IAppCache>(() => new CachingService());
    _api = RestService.For<T>(url, new RefitSettings
    {
      ExceptionFactory = CreateExceptionAsync
    });
  }

  public async Task<TResult> Call<TResult>(Func<T, Task<TResult>> itemFactory, Func<string>? cacheKeyFunc = null)
    where TResult : class?
  {
    if (!_cacheEnabled || cacheKeyFunc == null)
    {
      return await itemFactory(_api);
    }

    var cacheKey = cacheKeyFunc();
    return string.IsNullOrEmpty(cacheKey)
      ? await itemFactory(_api)
      : await _cache.Value.GetOrAddAsync(cacheKey, () => itemFactory(_api), _cacheDuration);
  }

  private static async Task<Exception?> CreateExceptionAsync(HttpResponseMessage response)
  {
    if (response.IsSuccessStatusCode) return null;

    return new ApiException(
      response.StatusCode,
      response.ReasonPhrase,
      await response.Content.ReadAsStringAsync().ConfigureAwait(false)
    );
  }
}