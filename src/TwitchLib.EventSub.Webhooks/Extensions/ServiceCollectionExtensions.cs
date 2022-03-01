using Microsoft.Extensions.DependencyInjection;
using System;
using TwitchLib.EventSub.Webhooks.Core;
using TwitchLib.EventSub.Webhooks.Core.Models;

namespace TwitchLib.EventSub.Webhooks.Extensions
{
    /// <summary>
    /// Extension methods for ServiceCollection
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds services for TwitchLib.EventSub.Webhooks to the specified IServiceCollection
        /// </summary>
        /// <param name="services">Reference to the IServiceCollection</param>
        /// <param name="config">An Action to configure TwitchLib.EventSub.Webhooks</param>
        /// <returns>A reference to the IServiceCollection after the operation has completed</returns>
        public static IServiceCollection AddTwitchLibEventSubWebhooks(this IServiceCollection services, Action<TwitchLibEventSubOptions> config)
        {
            services.Configure(config);
            services.AddSingleton<ITwitchEventSubWebhooks, TwitchEventSubWebhooks>();
            return services;
        }
    }
}