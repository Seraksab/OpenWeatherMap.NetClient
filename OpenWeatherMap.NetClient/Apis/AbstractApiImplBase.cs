using LazyCache;
using OpenWeatherMap.NetClient.Models;

namespace OpenWeatherMap.NetClient.Apis;

public abstract class AbstractApiImplBase
{
  private protected const string BaseUrl = "https://api.openweathermap.org";

  private readonly bool _cacheEnabled;
  private readonly TimeSpan _cacheDuration;
  private readonly Lazy<IAppCache> _cache;

  internal AbstractApiImplBase(IOpenWeatherMapOptions? options = null)
  {
    Language = (options?.Culture ?? OpenWeatherMapOptions.Defaults.Culture).TwoLetterISOLanguageName;

    _cacheEnabled = options?.CacheEnabled ?? OpenWeatherMapOptions.Defaults.CacheEnabled;
    _cacheDuration = options?.CacheDuration ?? OpenWeatherMapOptions.Defaults.CacheDuration;
    _cache = new Lazy<IAppCache>(() => new CachingService());
  }

  private protected string Language { get; }

  private protected async Task<IApiResponse<T>> CacheRequest<T>(
    Func<string> keyFunction,
    Func<Task<IApiResponse<T>>> itemFactory
  ) where T : class
  {
    return _cacheEnabled
      ? await _cache.Value.GetOrAddAsync(keyFunction(), itemFactory, _cacheDuration)
      : await itemFactory();
  }
}