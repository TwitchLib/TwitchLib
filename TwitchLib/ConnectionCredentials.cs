using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib
{
    public class ConnectionCredentials
    {
        public enum ClientType
        {
            CHAT,
            WHISPER
        }

        private string host, twitchUsername, twitchOAuth;
        private int port;

        public string Host { get { return host; } set { host = value; } }
        public string TwitchUsername { get { return twitchUsername; } set { twitchUsername = value; } }
        public string TwitchOAuth { get { return twitchOAuth; } set { twitchOAuth = value; }  }
        public int Port { get { return port; } set { port = value; } }

        public ConnectionCredentials(string host, int port, string twitchUsername, string twitchOAuth)
        {
            this.host = host;
            this.port = port;
            this.twitchUsername = twitchUsername.ToLower();
            this.twitchOAuth = twitchOAuth;
        }

        public ConnectionCredentials(ClientType type, TwitchIpAndPort tIpAndPort, string twitchUsername, string twitchOAuth)
        {
            if (type == ClientType.CHAT)
            {
                this.host = tIpAndPort.getFirstChatServer().IP;
                this.port = tIpAndPort.getFirstChatServer().Port;
            }
            else
            {
                this.host = tIpAndPort.getFirstWhisperServer().IP;
                this.port = tIpAndPort.getFirstWhisperServer().Port;
            }
            this.twitchUsername = twitchUsername.ToLower();
            this.twitchOAuth = twitchOAuth;
        }

    }
}
