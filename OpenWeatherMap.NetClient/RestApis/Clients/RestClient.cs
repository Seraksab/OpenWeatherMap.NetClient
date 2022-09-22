using System.Net;
using Microsoft.Extensions.Caching.Memory;
using OpenWeatherMap.NetClient.Models;
using Polly;
using Polly.Caching;
using Refit;
using ApiException = OpenWeatherMap.NetClient.Exceptions.ApiException;

namespace OpenWeatherMap.NetClient.RestApis.Clients;

internal sealed class RestClient<T> : IAsyncCacheProvider where T : class
{
  private readonly T _api;

  private readonly OpenWeatherMapOptions _options;
  private readonly IMemoryCache _cache;
  private readonly IAsyncPolicy _policy;

  internal RestClient(string url, OpenWeatherMapOptions options)
  {
    _options = options;
    _cache = new MemoryCache(new MemoryCacheOptions());
    _policy = GetPolicy();

    _api = RestService.For<T>(url, new RefitSettings
    {
      ExceptionFactory = CreateExceptionAsync
    });
  }

  public async Task<TResult> Call<TResult>(Func<T, Task<TResult>> itemFactory, Func<string>? cacheKeyFunc = null)
    where TResult : class?
  {
    // if cacheKey is 'null' the caching policy will not cache the result 
    var cacheKey = _options.CacheDuration.Ticks <= 0 ? null : cacheKeyFunc?.Invoke();
    return await _policy.ExecuteAsync(async _ => await itemFactory(_api), new Context(cacheKey));
  }

  public Task<(bool, object)> TryGetAsync(string key, CancellationToken ct, bool continueOnCapturedContext)
  {
    var found = _cache.TryGetValue<object>(key, out var value);
    return Task.FromResult((found, value));
  }

  public Task PutAsync(string key, object value, Ttl ttl, CancellationToken ct, bool continueOnCapturedContext)
  {
    _cache.Set(key, value, DateTimeOffset.Now + ttl.Timespan);
    return Task.CompletedTask;
  }

  private AsyncPolicy GetPolicy()
  {
    AsyncPolicy cachePolicy = _options.CacheDuration.Ticks <= 0
      ? Policy.NoOpAsync()
      : Policy.CacheAsync(this, _options.CacheDuration);

    AsyncPolicy retryPolicy = _options.RetryCount <= 0
      ? Policy.NoOpAsync()
      : Policy.Handle<ApiException>(e =>
          e.StatusCode is >= HttpStatusCode.InternalServerError or HttpStatusCode.RequestTimeout
        )
        .WaitAndRetryAsync(_options.RetryCount, _options.RetryWaitDurationProvider);

    return Policy.WrapAsync(cachePolicy, retryPolicy);
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