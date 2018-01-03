using Microsoft.AspNetCore.Authentication;
using System;

namespace TwitchLib.Extension.Core
{
    public static class TwitchExtensionAuthExtensions
    {
        public static AuthenticationBuilder AddTwitchExtensionAuth(this AuthenticationBuilder builder, Action<TwitchExtensionAuthOptions> configureOptions)
            => builder.AddTwitchExtensionAuth(TwitchExtensionAuthDefaults.AuthenticationScheme, configureOptions);

        public static AuthenticationBuilder AddTwitchExtensionAuth(this AuthenticationBuilder builder, string authenticationScheme, Action<TwitchExtensionAuthOptions> configureOptions)
            => builder.AddTwitchExtensionAuth(authenticationScheme, TwitchExtensionAuthDefaults.DisplayName, configureOptions);
        
        public static AuthenticationBuilder AddTwitchExtensionAuth(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<TwitchExtensionAuthOptions> configureOptions)
            => builder.AddScheme<TwitchExtensionAuthOptions, TwitchExtensionAuthHandler>(authenticationScheme, displayName, configureOptions);
    }
}
