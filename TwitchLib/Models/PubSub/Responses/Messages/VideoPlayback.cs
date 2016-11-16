using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.Models.PubSub.Responses.Messages
{
    public class VideoPlayback : MessageData
    {
        public enum TypeEnum
        {
            StreamUp,
            StreamDown,
            ViewCount
        }

        public TypeEnum Type { get; protected set; }
        public string ServerTime { get; protected set; }
        public int PlayDelay { get; protected set; }
        public int Viewers { get; protected set; }

        public VideoPlayback(string jsonStr)
        {
            JToken json = JObject.Parse(jsonStr);
            switch (json.SelectToken("type").ToString())
            {
                case "stream-up":
                    Type = TypeEnum.StreamUp;
                    break;
                case "stream-down":
                    Type = TypeEnum.StreamDown;
                    break;
                case "viewcount":
                    Type = TypeEnum.ViewCount;
                    break;
            }
            ServerTime = json.SelectToken("server_time")?.ToString();
            if (Type != TypeEnum.ViewCount)
                PlayDelay = int.Parse(json.SelectToken("play_delay").ToString());
            else
                Viewers = int.Parse(json.SelectToken("viewers").ToString());
        }
    }
}
