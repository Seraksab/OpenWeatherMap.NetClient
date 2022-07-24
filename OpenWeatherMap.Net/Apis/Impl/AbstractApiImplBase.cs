﻿using LazyCache;
using OpenWeatherMap.Net.Models;

namespace OpenWeatherMap.Net.Apis.Impl;

internal abstract class AbstractApiImplBase
{
  private protected const string BaseUrl = "https://api.openweathermap.org";

  private readonly TimeSpan _cacheDuration;
  private readonly IAppCache? _cache;

  internal AbstractApiImplBase(IOpenWeatherMapOptions? options = null)
  {
    Language = (options?.Culture ?? OpenWeatherMapOptions.Defaults.Culture).TwoLetterISOLanguageName;
    _cacheDuration = options?.CacheDuration ?? OpenWeatherMapOptions.Defaults.CacheDuration;
    if (options?.CacheEnabled ?? OpenWeatherMapOptions.Defaults.CacheEnabled)
    {
      _cache = new CachingService();
    }
  }

  private protected string Language { get; }

  private protected async Task<IApiResponse<T>> CacheRequest<T>(string cacheKey,
    Func<Task<Models.IApiResponse<T>>> itemFactory)
    where T : class
  {
    return _cache == null
      ? await itemFactory()
      : await _cache.GetOrAddAsync(cacheKey, itemFactory, _cacheDuration);
  }
}