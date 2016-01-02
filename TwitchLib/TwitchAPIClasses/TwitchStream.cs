using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.TwitchAPIClasses
{
    public class TwitchStream
    {
        private long id;
        private int viewers, video_height, delay;
        private string game, created_at;
        private bool is_playlist = false;
        private double average_fps;
        private TwitchChannel channel;
        private PreviewObj preview;

        public long ID { get { return id; } }
        public int Viewers { get { return viewers; } }
        public int VideoHeight { get { return video_height; } }
        public int Delay { get { return delay; } }
        public string Game { get { return game; } }
        public string CreatedAt { get { return created_at; } }
        public bool IsPlaylist { get { return is_playlist; } }
        public double AverageFPS { get { return average_fps; } }
        public TwitchChannel Channel { get { return channel; } }
        public PreviewObj Preview { get { return preview; } }

        public TwitchStream(JToken twitchStreamData)
        {
            id = long.Parse(twitchStreamData.SelectToken("_id").ToString());
            viewers = int.Parse(twitchStreamData.SelectToken("viewers").ToString());
            video_height = int.Parse(twitchStreamData.SelectToken("video_height").ToString());
            delay = int.Parse(twitchStreamData.SelectToken("delay").ToString());
            game = twitchStreamData.SelectToken("game").ToString();
            created_at = twitchStreamData.SelectToken("created_at").ToString();
            if (twitchStreamData.SelectToken("is_playlist").ToString() == "true")
                is_playlist = true;
            average_fps = double.Parse(twitchStreamData.SelectToken("average_fps").ToString());
            channel = new TwitchChannel(twitchStreamData.SelectToken("channel").ToString());
            preview = new PreviewObj(twitchStreamData.SelectToken("preview"));
        }

        public class PreviewObj
        {
            private string small, medium, large, template;

            public string Small { get { return small; } }
            public string Medium { get { return medium; } }
            public string Large { get { return large; } }
            public string Template { get { return template; } }

            public PreviewObj(JToken previewData)
            {
                small = previewData.SelectToken("small").ToString();
                medium = previewData.SelectToken("medium").ToString();
                large = previewData.SelectToken("large").ToString();
                template = previewData.SelectToken("template").ToString();
            }
        }
    }
}
