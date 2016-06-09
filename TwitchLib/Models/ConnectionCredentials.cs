namespace TwitchLib
{
    public class ConnectionCredentials
    {
        public enum ClientType
        {
            Chat,
            Whisper
        }

        public string Host { get; }

        public string TwitchUsername { get; }

        public string TwitchOAuth { get; }

        public int Port { get; }

        public ConnectionCredentials(string host, int port, string twitchUsername, string twitchOAuth)
        {
            Host = host;
            Port = port;
            TwitchUsername = twitchUsername.ToLower();
            TwitchOAuth = twitchOAuth;
        }

        public ConnectionCredentials(ClientType type, TwitchIpAndPort tIpAndPort, string twitchUsername,
            string twitchOAuth)
        {
            if (type == ClientType.Chat)
            {
                Host = tIpAndPort.GetFirstChatServer().Ip;
                Port = tIpAndPort.GetFirstChatServer().Port;
            }
            else
            {
                Host = tIpAndPort.GetFirstWhisperServer().Ip;
                Port = tIpAndPort.GetFirstWhisperServer().Port;
            }
            TwitchUsername = twitchUsername.ToLower();
            TwitchOAuth = twitchOAuth;
        }
    }
}