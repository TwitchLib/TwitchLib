using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib
{
    public class ConnectionCredentials
    {
        private string chatHost, whisperHost, twitchUsername, twitchOAuth;
        private int chatPort, whisperPort;

        public string ChatHost { get { return chatHost; } set { chatHost = value; } }
        public string WhisperHost { get { return whisperHost; } set { whisperHost = value; } }
        public string TwitchUsername { get { return twitchUsername; } set { twitchUsername = value; } }
        public string TwitchOAuth { get { return twitchOAuth; } set { twitchOAuth = value; }  }
        public int ChatPort { get { return chatPort; } set { chatPort = value; } }
        public int WhisperPort { get { return whisperPort; } set { whisperPort = value; } }

        public ConnectionCredentials(string chatHost, int chatPort, string whisperHost, int whisperPort, string twitchUsername, string twitchOAuth)
        {
            this.chatHost = chatHost;
            this.chatPort = chatPort;
            this.twitchUsername = twitchUsername;
            this.twitchOAuth = twitchOAuth;
            this.whisperHost = whisperHost;
            this.whisperPort = whisperPort;
        }

    }
}
