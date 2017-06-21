using System.Text.RegularExpressions;

namespace TwitchLib.Models.Client
{
    /// <summary>Class used to store credentials used to connect to Twitch chat/whisper.</summary>
    public class ConnectionCredentials
    {
        /// <summary>Property representing bot's username.</summary>
        public string TwitchUsername { get; set; }
        /// <summary>Property representing bot's oauth.</summary>
        public string TwitchOAuth { get; set; }
        /// <summary>Property representing Twitch's host address</summary>
        public string TwitchHost { get; set; }
        /// <summary>Property representing Twitch's host port</summary>
        public int TwitchPort { get; set; }

        /// <summary>Constructor for ConnectionCredentials object.</summary>
        public ConnectionCredentials(string twitchUsername, string twitchOAuth, string twitchHost = "irc-ws.chat.twitch.tv", int twitchPort = 80, bool disableUsernameCheck = false)
        {
            if (!disableUsernameCheck && !(new Regex("^([a-zA-Z0-9][a-zA-Z0-9_]{3,25})$").Match(twitchUsername).Success))
                throw new Exceptions.Client.ErrorLoggingInException("Twitch username does not appear to be valid.", twitchUsername);
            TwitchUsername = twitchUsername.ToLower();
            TwitchOAuth = twitchOAuth;
            TwitchHost = twitchHost;
            TwitchPort = twitchPort;
        }
    }
}