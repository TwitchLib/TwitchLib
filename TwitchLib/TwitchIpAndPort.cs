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
        private string channel;
        private IPPort[] chatServers, whisperServers;

        public TwitchIpAndPort(string channel, bool autoDownloadServerData = false)
        {
            this.channel = channel;
            if (autoDownloadServerData)
            {
                getChatServers();
                getWhisperServers();
            }

        }

        public IPPort getFirstChatServer()
        {
            if (chatServers != null)
                return chatServers[0];
            return null;
        }

        public IPPort getFirstWhisperServer()
        {
            if (whisperServers != null)
                return whisperServers[0];
            return null;
        }

        public IPPort[] getChatServers()
        {
            string cnts = new System.Net.WebClient().DownloadString(String.Format("https://api.twitch.tv/api/channels/{0}/chat_properties", channel));

            JObject json = JObject.Parse(cnts);
            chatServers = new IPPort[json.SelectToken("chat_servers").Count()];
            for (int i = 0; i < chatServers.Count(); i++)
            {
                chatServers[i] = new IPPort(json.SelectToken("chat_servers")[i].ToString());
            }
            return chatServers;
        }

        public IPPort[] getWhisperServers()
        {
            string cnts = new System.Net.WebClient().DownloadString("http://tmi.twitch.tv/servers?cluster=group");

            JObject json = JObject.Parse(cnts);
            whisperServers = new IPPort[json.SelectToken("servers").Count()];
            for (int i = 0; i < whisperServers.Count(); i++)
            {
                whisperServers[i] = new IPPort(json.SelectToken("servers")[i].ToString());
            }
            return whisperServers;
        }
    }

    public class IPPort
    {
        private string ip;
        private int port;

        public string IP { get { return ip; } }
        public int Port { get { return port; } }
        public IPPort(string data)
        {
            ip = data.Split(':')[0];
            port = int.Parse(data.Split(':')[1]);
        }
    }
}
