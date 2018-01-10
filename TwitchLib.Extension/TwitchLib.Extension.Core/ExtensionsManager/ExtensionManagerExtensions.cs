using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace TwitchLib.Extension.Core.ExtensionsManager
{
    public static class ExtensionManagerExtensions
    {
        public static IServiceCollection AddTwitchExtensionManager(this IServiceCollection services)
        {

            if (!services.Any(x => x.ServiceType == typeof(API)))
            {
                services.AddSingleton<API>();
            }

            if (!services.Any(x => x.ServiceType == typeof(Extensions)))
            {
                services.AddSingleton<Extensions>();
            }

            if (!services.Any(x => x.ServiceType == typeof(ExtensionManager)))
            {
                services.AddSingleton<ExtensionManager>();
            }
            return services;
        }

        public static IApplicationBuilder UseTwitchExtensionManager(this IApplicationBuilder app, IServiceProvider serviceProvider, ExtensionsConfiguration options)
        {
            var extensions = serviceProvider.GetService<Extensions>();

            extensions.Extension = options.Extensions.ToDictionary(cfg => cfg.Id, cfg => new Extension(serviceProvider.GetService<API>(), cfg));

            return app;
        }
    }
}
