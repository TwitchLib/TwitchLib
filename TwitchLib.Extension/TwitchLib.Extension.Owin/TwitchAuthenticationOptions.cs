using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace TwitchLib.Extension.Owin
{ 
    public class TwitchAuthenticationOptions : AuthenticationOptions
    {
        public class TwitchAuthenticationEndpoints
        {
            /// <summary>
            /// Endpoint which is used to redirect users to request Twitch access
            /// </summary>
            /// <remarks>
            /// Defaults to https://api.twitch.tv/kraken/oauth2/authorize
            /// </remarks>
            public string AuthorizationEndpoint { get; set; }

            /// <summary>
            /// Endpoint which is used to exchange code for access token
            /// </summary>
            /// <remarks>
            /// Defaults to https://api.twitch.tv/kraken/oauth2/token
            /// </remarks>
            public string TokenEndpoint { get; set; }

            /// <summary>
            /// Endpoint which is used to obtain user information after authentication
            /// </summary>
            /// <remarks>
            /// Defaults to https://api.twitch.tv/kraken/user
            /// </remarks>
            public string UserInfoEndpoint { get; set; }
        }

        private const string AuthorizationEndPoint = "https://api.twitch.tv/kraken/oauth2/authorize";
        private const string TokenEndpoint = "https://api.twitch.tv/kraken/oauth2/token";
        private const string UserInfoEndpoint = "https://api.twitch.tv/kraken/user";

        /// <summary>
        ///     Gets or sets the a pinned certificate validator to use to validate the endpoints used
        ///     in back channel communications belong to Twitch.
        /// </summary>
        /// <value>
        ///     The pinned certificate validator.
        /// </value>
        /// <remarks>
        ///     If this property is null then the default certificate checks are performed,
        ///     validating the subject name and if the signing chain is a trusted party.
        /// </remarks>
        public ICertificateValidator BackchannelCertificateValidator { get; set; }

        /// <summary>
        ///     The HttpMessageHandler used to communicate with Twitch.
        ///     This cannot be set at the same time as BackchannelCertificateValidator unless the value
        ///     can be downcast to a WebRequestHandler.
        /// </summary>
        public HttpMessageHandler BackchannelHttpHandler { get; set; }

        /// <summary>
        ///     Gets or sets timeout value in milliseconds for back channel communications with Twitch.
        /// </summary>
        /// <value>
        ///     The back channel timeout in milliseconds.
        /// </value>
        public TimeSpan BackchannelTimeout { get; set; }

        /// <summary>
        ///     The request path within the application's base path where the user-agent will be returned.
        ///     The middleware will process this request when it arrives.
        ///     Default value is "/signin-Twitch".
        /// </summary>
        public PathString CallbackPath { get; set; }

        /// <summary>
        ///     Get or sets the text that the user can display on a sign in user interface.
        /// </summary>
        public string Caption
        {
            get { return Description.Caption; }
            set { Description.Caption = value; }
        }

        /// <summary>
        ///     Gets or sets the Twitch supplied Client ID
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        ///     Gets or sets the Twitch supplied Client Secret
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Gets the sets of OAuth endpoints used to authenticate against Twitch.  Overriding these endpoints allows you to use Twitch Enterprise for
        /// authentication.
        /// </summary>
        public TwitchAuthenticationEndpoints Endpoints { get; set; }

        /// <summary>
        ///     Gets or sets the <see cref="ITwitchAuthenticationProvider" /> used in the authentication events
        /// </summary>
        public ITwitchAuthenticationProvider Provider { get; set; }

        /// <summary>
        /// A list of permissions to request.
        /// </summary>
        public IList<string> Scope { get; private set; }

        /// <summary>
        ///     Gets or sets the name of another authentication middleware which will be responsible for actually issuing a user
        ///     <see cref="System.Security.Claims.ClaimsIdentity" />.
        /// </summary>
        public string SignInAsAuthenticationType { get; set; }

        /// <summary>
        ///     Gets or sets the type used to secure data handled by the middleware.
        /// </summary>
        public ISecureDataFormat<AuthenticationProperties> StateDataFormat { get; set; }

        /// <summary>
        ///     Gets or sets whether to force verify the user during the OAuth flow
        /// </summary>
        public bool ForceVerify { get; set; }

        /// <summary>
        ///     Initializes a new <see cref="TwitchAuthenticationOptions" />
        /// </summary>
        public TwitchAuthenticationOptions()
            : base("Twitch")
        {
            Caption = Constants.DefaultAuthenticationType;
            CallbackPath = new PathString("/signin-Twitch");
            AuthenticationMode = AuthenticationMode.Passive;
            Scope = new List<string>
            {
                "user_read"
            };
            BackchannelTimeout = TimeSpan.FromSeconds(60);
            Endpoints = new TwitchAuthenticationEndpoints
            {
                AuthorizationEndpoint = AuthorizationEndPoint,
                TokenEndpoint = TokenEndpoint,
                UserInfoEndpoint = UserInfoEndpoint
            };
        }
    }
}