using Microsoft.Extensions.DependencyInjection;
using OpenWeatherMap.NetClient.Models;

namespace OpenWeatherMap.NetClient.Extensions;

/// <summary>
/// Extension for <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtension
{
  /// <summary>
  /// Adds a singleton <see cref="IOpenWeatherMap"/> service for dependency injection.
  /// </summary>
  /// <param name="services">The interface being extended</param>
  /// <param name="apiKey">The unique OpenWeatherMap API key</param>
  /// <param name="options">Optional client configuration</param>
  /// <returns>The service collection</returns>
  public static IServiceCollection AddOpenWeatherMap(
    this IServiceCollection services,
    string apiKey,
    IOpenWeatherMapOptions? options = null
  )
  {
    services.AddSingleton<IOpenWeatherMap>(new OpenWeatherMapClient(apiKey, options));
    return services;
  }
}