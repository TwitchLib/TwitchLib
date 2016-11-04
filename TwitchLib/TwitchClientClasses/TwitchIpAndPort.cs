using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib
{
    /// <summary>Class representing ip and port connection details.</summary>
    public class TwitchIpAndPort
    {
        private IpPort[] _chatServers, _whisperServers;

        /// <summary>
        /// Constructor for TwitchIpAndPort requiring channel and bool for autodownload.
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="autoDownloadServerData"></param>
        public TwitchIpAndPort(string channel, string app_id, bool autoDownloadServerData = false)
        {
            if (!autoDownloadServerData) return;
            GetChatServers(channel, app_id);
            GetWhisperServers();
        }

        /// <summary>
        /// Constructor for TwitchIpAndPort requiring simply a bool for auto download.
        /// </summary>
        /// <param name="autoDownloadServerData"></param>
        public TwitchIpAndPort(bool autoDownloadServerData = false)
        {
            if (autoDownloadServerData)
            {
                GetWhisperServers();
            }
        }

        /// <summary>Returns first chat server found, can be null.</summary>
        public IpPort GetFirstChatServer()
        {
            return _chatServers?[0];
        }

        /// <summary>Returns first whisper server found, can be null.</summary>
        public IpPort GetFirstWhisperServer()
        {
            return _whisperServers?[0];
        }

        /// <summary>Downloads array of IpPort objects for chat servers.</summary>
        public IpPort[] GetChatServers(string channel, string app_id)
        {
            var resp = new WebClient().DownloadString(
                $"https://api.twitch.tv/api/channels/{channel}/chat_properties?client_id={app_id}");

            var json = JObject.Parse(resp);
            _chatServers = new IpPort[json.SelectToken("chat_servers").Count()];
            for (var i = 0; i < _chatServers.Length; i++)
            {
                _chatServers[i] = new IpPort(json.SelectToken("chat_servers")[i].ToString());
            }
            return _chatServers;
        }

        /// <summary>Downloads array of IpPort objects for whisper servers.</summary>
        public IpPort[] GetWhisperServers()
        {
            var resp = new WebClient().DownloadString("http://tmi.twitch.tv/servers?cluster=group");

            var json = JObject.Parse(resp);
            _whisperServers = new IpPort[json.SelectToken("servers").Count()];
            for (var i = 0; i < _whisperServers.Length; i++)
            {
                _whisperServers[i] = new IpPort(json.SelectToken("servers")[i].ToString());
            }
            return _whisperServers;
        }
    }

    /// <summary>Class representing Ip and Port for connections.</summary>
    public class IpPort
    {
        /// <summary>Property representing Ip.</summary>
        public string Ip { get; protected set; }
        /// <summary>Property representing Port</summary>
        public int Port { get; protected set; }

        /// <summary>Constructor for IpPort requiring data string.</summary>
        public IpPort(string data)
        {
            Ip = data.Split(':')[0];
            Port = int.Parse(data.Split(':')[1]);
        }
    }
}
