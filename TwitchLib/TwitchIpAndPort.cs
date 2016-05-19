using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib
{
    public class TwitchIpAndPort
    {
        private IpPort[] _chatServers, _whisperServers;

        public TwitchIpAndPort(string channel, bool autoDownloadServerData = false)
        {
            if (!autoDownloadServerData) return;
            GetChatServers(channel);
            GetWhisperServers();
        }

        public TwitchIpAndPort(bool autoDownloadServerData = false)
        {
            if (autoDownloadServerData)
            {
                GetWhisperServers();
            }
        }

        public IpPort GetFirstChatServer()
        {
            return _chatServers?[0];
        }

        public IpPort GetFirstWhisperServer()
        {
            return _whisperServers?[0];
        }

        public IpPort[] GetChatServers(string channel)
        {
            string cnts = new System.Net.WebClient().DownloadString(
                $"https://api.twitch.tv/api/channels/{channel}/chat_properties");

            JObject json = JObject.Parse(cnts);
            _chatServers = new IpPort[json.SelectToken("chat_servers").Count()];
            for (int i = 0; i < _chatServers.Length; i++)
            {
                _chatServers[i] = new IpPort(json.SelectToken("chat_servers")[i].ToString());
            }
            return _chatServers;
        }

        public IpPort[] GetWhisperServers()
        {
            string cnts = new System.Net.WebClient().DownloadString("http://tmi.twitch.tv/servers?cluster=group");

            JObject json = JObject.Parse(cnts);
            _whisperServers = new IpPort[json.SelectToken("servers").Count()];
            for (int i = 0; i < _whisperServers.Length; i++)
            {
                _whisperServers[i] = new IpPort(json.SelectToken("servers")[i].ToString());
            }
            return _whisperServers;
        }
    }

    public class IpPort
    {
        private string _ip;
        private int _port;

        public string Ip => _ip;
        public int Port => _port;

        public IpPort(string data)
        {
            _ip = data.Split(':')[0];
            _port = int.Parse(data.Split(':')[1]);
        }
    }
}