using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib
{
    public class ConnectionCredentials
    {
        public string TwitchUsername { get; set; }
        public string TwitchOAuth { get; set; }

        public string TwitchHost { get; set; } = "irc.chat.twitch.tv";
        public int TwitchPort { get; set; } = 6667;

        public ConnectionCredentials(string twitchUsername, string twitchOAuth)
        {
            TwitchUsername = twitchUsername.ToLower();
            TwitchOAuth = twitchOAuth;
        }
    }
}