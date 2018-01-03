using Owin;
using System;

namespace TwitchLib.Extension.Owin
{
    public static class TwitchAuthenticationExtensions
    {
        /// <summary>
        ///  Login with Twitch. http://yourUrl/signin-Twitch will be used as the redirect URI
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IAppBuilder UseTwitchAuthentication(this IAppBuilder app,
            TwitchAuthenticationOptions options)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            app.Use(typeof(TwitchAuthenticationMiddleware), app, options);

            return app;
        }
        /// <summary>
        /// Login with Twitch. http://yourUrl/signin-Twitch will be used as the redirect URI
        /// </summary>
        /// <param name="app"></param>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <returns></returns>
        public static IAppBuilder UseTwitchAuthentication(this IAppBuilder app, string clientId, string clientSecret)
        {
            return app.UseTwitchAuthentication(new TwitchAuthenticationOptions
            {
                ClientId = clientId,
                ClientSecret = clientSecret
            });
        }
    }
}