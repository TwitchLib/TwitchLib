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
        private long _id;
        private int _viewers, _videoHeight, _delay;
        private string _game, _createdAt;
        private bool _isPlaylist = false;
        private double _averageFps;
        private TwitchChannel _channel;
        private PreviewObj _preview;

        public long Id { get { return _id; } }
        public int Viewers { get { return _viewers; } }
        public int VideoHeight { get { return _videoHeight; } }
        public int Delay { get { return _delay; } }
        public string Game { get { return _game; } }
        public string CreatedAt { get { return _createdAt; } }
        public bool IsPlaylist { get { return _isPlaylist; } }
        public double AverageFps { get { return _averageFps; } }
        public TwitchChannel Channel { get { return _channel; } }
        public PreviewObj Preview { get { return _preview; } }

        public TwitchStream(JToken twitchStreamData)
        {
            _id = long.Parse(twitchStreamData.SelectToken("_id").ToString());
            _viewers = int.Parse(twitchStreamData.SelectToken("viewers").ToString());
            _videoHeight = int.Parse(twitchStreamData.SelectToken("video_height").ToString());
            _delay = int.Parse(twitchStreamData.SelectToken("delay").ToString());
            _game = twitchStreamData.SelectToken("game").ToString();
            _createdAt = twitchStreamData.SelectToken("created_at").ToString();
            if (twitchStreamData.SelectToken("is_playlist").ToString() == "true")
                _isPlaylist = true;
            _averageFps = double.Parse(twitchStreamData.SelectToken("average_fps").ToString());
            _channel = new TwitchChannel((JObject)twitchStreamData.SelectToken("channel"));
            _preview = new PreviewObj(twitchStreamData.SelectToken("preview"));
        }

        public class PreviewObj
        {
            private string _small, _medium, _large, _template;

            public string Small { get { return _small; } }
            public string Medium { get { return _medium; } }
            public string Large { get { return _large; } }
            public string Template { get { return _template; } }

            public PreviewObj(JToken previewData)
            {
                _small = previewData.SelectToken("small").ToString();
                _medium = previewData.SelectToken("medium").ToString();
                _large = previewData.SelectToken("large").ToString();
                _template = previewData.SelectToken("template").ToString();
            }
        }
    }
}
