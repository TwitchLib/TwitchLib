using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.TwitchAPIClasses
{
    public class TwitchVideo
    {
        string _title, _description, _status, _id, _tagList, _recordedAt, _game, _preview, _broadcastId, _url;
        int _length, _views;
        FpsData _fps;
        ResolutionsData _resolutions;
        ChannelData _channel;

        public ChannelData Channel => _channel;
        public FpsData Fps => _fps;
        public int Length => _length;
        public int Views => _views;
        public ResolutionsData Resolutions => _resolutions;
        public string BroadcastId => _broadcastId;
        public string Description => _description;
        public string Game => _game;
        public string Id => _id;
        public string Preview => _preview;
        public string RecordedAt => _recordedAt;
        public string Status => _status;
        public string TagList => _tagList;
        public string Title => _title;
        public string Url => _url;

        public TwitchVideo(JToken apiResponse)
        {
            int length, views, audioFps;
            double mediumFps, mobileFps, highFps, lowFps, chunkedFps;

            if (int.TryParse(apiResponse.SelectToken("length").ToString(), out length)) _length = length;
            if (int.TryParse(apiResponse.SelectToken("views").ToString(), out views)) _views = views;

            int.TryParse(apiResponse.SelectToken("fps").SelectToken("audio_only").ToString(), out audioFps);
            double.TryParse(apiResponse.SelectToken("fps").SelectToken("medium").ToString(), out mediumFps);
            double.TryParse(apiResponse.SelectToken("fps").SelectToken("mobile").ToString(), out mobileFps);
            double.TryParse(apiResponse.SelectToken("fps").SelectToken("high").ToString(), out highFps);
            double.TryParse(apiResponse.SelectToken("fps").SelectToken("low").ToString(), out lowFps);
            double.TryParse(apiResponse.SelectToken("fps").SelectToken("chunked").ToString(), out chunkedFps);

            _broadcastId = apiResponse.SelectToken("broadcast_id").ToString();
            _description = apiResponse.SelectToken("description").ToString();
            _fps = new FpsData(audioFps, mediumFps, mobileFps, highFps, lowFps, chunkedFps);
            _game = apiResponse.SelectToken("game").ToString();
            _id = apiResponse.SelectToken("_id").ToString();
            _preview = apiResponse.SelectToken("preview").ToString();
            _recordedAt = apiResponse.SelectToken("recorded_at").ToString();
            _status = apiResponse.SelectToken("status").ToString();
            _tagList = apiResponse.SelectToken("tag_list").ToString();
            _title = apiResponse.SelectToken("title").ToString();
            _url = apiResponse.SelectToken("url").ToString();

            _resolutions = new ResolutionsData(apiResponse.SelectToken("resolutions").SelectToken("medium").ToString(),
                apiResponse.SelectToken("resolutions").SelectToken("mobile").ToString(),
                apiResponse.SelectToken("resolutions").SelectToken("high").ToString(),
                apiResponse.SelectToken("resolutions").SelectToken("low").ToString(),
                apiResponse.SelectToken("resolutions").SelectToken("chunked").ToString());
            _channel = new ChannelData(apiResponse.SelectToken("channel").SelectToken("name").ToString(),
                apiResponse.SelectToken("channel").SelectToken("display_name").ToString());
        }

        public class FpsData
        {
            private double _audioOnly, _medium, _mobile, _high, _low, _chunked;

            public double AudioOnly => _audioOnly;
            public double Medium => _medium;
            public double Mobile => _mobile;
            public double High => _high;
            public double Low => _low;
            public double Chunked => _chunked;

            public FpsData(double audioOnly, double medium, double mobile, double high, double low, double chunked)
            {
                _audioOnly = audioOnly;
                _low = low;
                _medium = medium;
                _mobile = mobile;
                _high = high;
                _chunked = chunked;
            }

            public override string ToString()
            {
                return
                    $"audio only: {_audioOnly}, mobile: {_mobile}, low: {_low}, medium: {_medium}, high: {_high}, chunked: {_chunked}";
            }
        }

        public class ResolutionsData
        {
            string _medium, _mobile, _high, _low, _chunked;

            public string Medium => _medium;
            public string Mobile => _mobile;
            public string High => _high;
            public string Low => _low;
            public string Chunked => _chunked;

            public ResolutionsData(string medium, string mobile, string high, string low, string chunked)
            {
                _medium = medium;
                _mobile = mobile;
                _high = high;
                _low = low;
                _chunked = chunked;
            }

            public override string ToString()
            {
                return $"mobile: {_mobile}, low: {_low}, medium: {_medium}, high: {_high}, chunked: {_chunked}";
            }
        }

        public class ChannelData
        {
            string _name, _displayName;

            public string Name => _name;
            public string DisplayName => _displayName;

            public ChannelData(string name, string displayName)
            {
                _name = name;
                _displayName = displayName;
            }

            public override string ToString()
            {
                return $"{_name}, {_displayName}";
            }
        }
    }
}