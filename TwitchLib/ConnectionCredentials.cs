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
            Chat,
            Whisper
        }

        private string _host, _twitchUsername, _twitchOAuth;
        private int _port;

        public string Host { get { return _host; } set { _host = value; } }
        public string TwitchUsername { get { return _twitchUsername; } set { _twitchUsername = value; } }
        public string TwitchOAuth { get { return _twitchOAuth; } set { _twitchOAuth = value; }  }
        public int Port { get { return _port; } set { _port = value; } }

        public ConnectionCredentials(string host, int port, string twitchUsername, string twitchOAuth)
        {
            this._host = host;
            this._port = port;
            this._twitchUsername = twitchUsername.ToLower();
            this._twitchOAuth = twitchOAuth;
        }

        public ConnectionCredentials(ClientType type, TwitchIpAndPort tIpAndPort, string twitchUsername, string twitchOAuth)
        {
            if (type == ClientType.Chat)
            {
                this._host = tIpAndPort.GetFirstChatServer().Ip;
                this._port = tIpAndPort.GetFirstChatServer().Port;
            }
            else
            {
                this._host = tIpAndPort.GetFirstWhisperServer().Ip;
                this._port = tIpAndPort.GetFirstWhisperServer().Port;
            }
            this._twitchUsername = twitchUsername.ToLower();
            this._twitchOAuth = twitchOAuth;
        }

    }
}
