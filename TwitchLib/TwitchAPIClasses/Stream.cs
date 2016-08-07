using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.TwitchAPIClasses
{
    public class Stream
    {
        public bool IsPlaylist { get; protected set; }
        public double AverageFps { get; protected set; }
        public int Delay { get; protected set; }
        public int VideoHeight { get; protected set; }
        public int Viewers { get; protected set; }
        public long Id { get; protected set; }
        public PreviewObj Preview { get; protected set; }
        public string CreatedAt { get; protected set; }
        public string Game { get; protected set; }
        public Channel Channel { get; protected set; }

        public Stream(JToken twitchStreamData)
        {
            bool isPlaylist;
            long id;
            int viewers, videoHeight, delay;
            double averageFps;

            if (bool.TryParse(twitchStreamData.SelectToken("is_playlist").ToString(), out isPlaylist) && isPlaylist) IsPlaylist = true;
            if (long.TryParse(twitchStreamData.SelectToken("_id").ToString(), out id)) Id = id;
            if (int.TryParse(twitchStreamData.SelectToken("viewers").ToString(), out viewers)) Viewers = viewers;
            if (int.TryParse(twitchStreamData.SelectToken("video_height").ToString(), out videoHeight)) VideoHeight = videoHeight;
            if (int.TryParse(twitchStreamData.SelectToken("delay").ToString(), out delay)) Delay = delay;
            if (double.TryParse(twitchStreamData.SelectToken("average_fps").ToString(), out averageFps)) AverageFps = averageFps;

            Game = twitchStreamData.SelectToken("game").ToString();
            CreatedAt = twitchStreamData.SelectToken("created_at").ToString();

            Channel = new Channel((JObject) twitchStreamData.SelectToken("channel"));
            Preview = new PreviewObj(twitchStreamData.SelectToken("preview"));
        }

        public class PreviewObj
        {
            public string Small { get; protected set; }
            public string Medium { get; protected set; }
            public string Large { get; protected set; }
            public string Template { get; protected set; }

            public PreviewObj(JToken previewData)
            {
                Small = previewData.SelectToken("small").ToString();
                Medium = previewData.SelectToken("medium").ToString();
                Large = previewData.SelectToken("large").ToString();
                Template = previewData.SelectToken("template").ToString();
            }
        }
    }
}