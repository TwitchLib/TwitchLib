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
        int _viewers, _videoHeight, _delay;
        private string _game, _createdAt;
        private bool _isPlaylist;
        private double _averageFps;
        private TwitchChannel _channel;
        private PreviewObj _preview;

        public bool IsPlaylist => _isPlaylist;
        public double AverageFps => _averageFps;
        public int Delay => _delay;
        public int VideoHeight => _videoHeight;
        public int Viewers => _viewers;
        public long Id => _id;
        public PreviewObj Preview => _preview;
        public string CreatedAt => _createdAt;
        public string Game => _game;
        public TwitchChannel Channel => _channel;

        public TwitchStream(JToken twitchStreamData)
        {
            bool isPlaylist;
            long id;
            int viewers, videoHeight, delay;
            double averageFps;

            if (bool.TryParse(twitchStreamData.SelectToken("is_playlist").ToString(), out isPlaylist) && isPlaylist) _isPlaylist = true;
            if (long.TryParse(twitchStreamData.SelectToken("_id").ToString(), out id)) _id = id;
            if (int.TryParse(twitchStreamData.SelectToken("viewers").ToString(), out viewers)) _viewers = viewers;
            if (int.TryParse(twitchStreamData.SelectToken("video_height").ToString(), out videoHeight)) _videoHeight = videoHeight;
            if (int.TryParse(twitchStreamData.SelectToken("delay").ToString(), out delay)) _delay = delay;
            if (double.TryParse(twitchStreamData.SelectToken("average_fps").ToString(), out averageFps)) _averageFps = averageFps;

            _game = twitchStreamData.SelectToken("game").ToString();
            _createdAt = twitchStreamData.SelectToken("created_at").ToString();

            _channel = new TwitchChannel((JObject) twitchStreamData.SelectToken("channel"));
            _preview = new PreviewObj(twitchStreamData.SelectToken("preview"));
        }

        public class PreviewObj
        {
            private string _small, _medium, _large, _template;

            public string Small => _small;
            public string Medium => _medium;
            public string Large => _large;
            public string Template => _template;

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