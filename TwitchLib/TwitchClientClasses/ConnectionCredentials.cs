using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib
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
        public ConnectionCredentials(string twitchUsername, string twitchOAuth, string twitchHost = "irc.chat.twitch.tv", int twitchPort = 6667)
        {
            TwitchUsername = twitchUsername.ToLower();
            TwitchOAuth = twitchOAuth;
            TwitchHost = twitchHost;
            TwitchPort = twitchPort;
        }
    }
}