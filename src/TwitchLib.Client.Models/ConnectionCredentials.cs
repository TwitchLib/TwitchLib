using System;
using System.Text.RegularExpressions;

namespace TwitchLib.Client.Models
{
    /// <summary>Class used to store credentials used to connect to Twitch chat/whisper.</summary>
    public class ConnectionCredentials
    {
        public const string DefaultWebSocketUri = "wss://irc-ws.chat.twitch.tv:443";

        /// <summary>Property representing URI used to connect to Twitch websocket service.</summary>
        public string TwitchWebsocketURI { get; }

        /// <summary>Property representing bot's oauth.</summary>
        public string TwitchOAuth { get; }

        /// <summary>Property representing bot's username.</summary>
        public string TwitchUsername { get; }

        /// <summary>Property representing capability requests sent to twitch.</summary>
        public Capabilities Capabilities { get; }

        /// <summary>Constructor for ConnectionCredentials object.</summary>
        public ConnectionCredentials(
            string twitchUsername,
            string twitchOAuth,
            string twitchWebsocketURI = DefaultWebSocketUri,
            bool disableUsernameCheck = false,
            Capabilities capabilities = null)
        {
            if (!disableUsernameCheck && !new Regex("^([a-zA-Z0-9][a-zA-Z0-9_]{3,25})$").Match(twitchUsername).Success)
                throw new Exception($"Twitch username does not appear to be valid. {twitchUsername}");

            TwitchUsername = twitchUsername.ToLower();
            TwitchOAuth = twitchOAuth;

            // Make sure proper formatting is applied to oauth
            if (!twitchOAuth.Contains(":"))
            {
                TwitchOAuth = $"oauth:{twitchOAuth.Replace("oauth", "")}";
            }

            TwitchWebsocketURI = twitchWebsocketURI;

            if (capabilities == null)
                capabilities = new Capabilities();
            Capabilities = capabilities;
        }
    }

    /// <summary>Class used to store capacity request settings used when connecting to Twitch</summary>
    public class Capabilities
    {
        /// <summary>Adds membership state event data. By default, we do not send this data to clients without this capability.</summary>
        public bool Membership { get; }

        /// <summary>Adds IRC V3 message tags to several commands, if enabled with the commands capability.</summary>
        public bool Tags { get; }

        /// <summary>Enables several Twitch-specific commands.</summary>
        public bool Commands { get; }

        public Capabilities(bool membership = true, bool tags = true, bool commands = true)
        {
            Membership = membership;
            Tags = tags;
            Commands = commands;
        }
    }
}
