using System;
using Newtonsoft.Json.Linq;
using TwitchLib.Enums;

namespace TwitchLib.Models.PubSub.Responses.Messages
{
    /// <inheritdoc />
    /// <summary>VideoPlayback model constructor.</summary>
    public class VideoPlayback : MessageData
    {
        /// <summary>Video playback type</summary>
        public VideoPlaybackType Type { get; protected set; }
        /// <summary>Server time stamp</summary>
        public string ServerTime { get; protected set; }
        /// <summary>Current delay (if one exists)</summary>
        public int PlayDelay { get; protected set; }
        /// <summary>Viewer count</summary>
        public int Viewers { get; protected set; }

        /// <summary>VideoPlayback constructor.</summary>
        /// <param name="jsonStr"></param>
        public VideoPlayback(string jsonStr)
        {
            JToken json = JObject.Parse(jsonStr);
            switch (json.SelectToken("type").ToString())
            {
                case "stream-up":
                    Type = VideoPlaybackType.StreamUp;
                    break;
                case "stream-down":
                    Type = VideoPlaybackType.StreamDown;
                    break;
                case "viewcount":
                    Type = VideoPlaybackType.ViewCount;
                    break;
            }
            ServerTime = json.SelectToken("server_time")?.ToString();
            switch (Type)
            {
                case VideoPlaybackType.StreamUp:
                    PlayDelay = int.Parse(json.SelectToken("play_delay").ToString());
                    break;
                case VideoPlaybackType.ViewCount:
                    Viewers = int.Parse(json.SelectToken("viewers").ToString());
                    break;
                case VideoPlaybackType.StreamDown:
                    break;
            }
        }
    }
}
