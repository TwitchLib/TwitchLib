using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TwitchLib.EventSub.Websockets.Client;
using TwitchLib.EventSub.Websockets.Core.Handler;

namespace TwitchLib.EventSub.Websockets.Extensions;

/// <summary>
/// Static class containing extension methods for the IServiceCollection of the DI Container
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services">ServiceCollection of the DI Container</param>
    /// <param name="scanMarkers">Array of types in which assemblies to search for NotificationHandlers</param>
    /// <returns>the IServiceCollection to enable further fluent additions to it</returns>
    private static IServiceCollection AddNotificationHandlers(this IServiceCollection services, params Type[] scanMarkers)
    {
        foreach (var marker in scanMarkers)
        {
            var types = marker
                .Assembly.DefinedTypes
                .Where(x => typeof(INotificationHandler).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .ToList();

            foreach (var type in types)
                services.AddSingleton(typeof(INotificationHandler), type);
        }

        return services;
    }

    /// <summary>
    /// Add TwitchLib EventSub Websockets and its needed parts to the DI container
    /// </summary>
    /// <param name="services">ServiceCollection of the DI Container</param>
    /// <returns>the IServiceCollection to enable further fluent additions to it</returns>
    public static IServiceCollection AddTwitchLibEventSubWebsockets(this IServiceCollection services)
    {
        services.TryAddTransient<WebsocketClient>();
        services.TryAddSingleton<EventSubWebsocketClient>();
        services.AddNotificationHandlers(typeof(INotificationHandler));
        return services;
    }
}