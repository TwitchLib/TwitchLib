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

        public string Title => _title;
        public string Description => _description;
        public string Status => _status;
        public string Id => _id;
        public string TagList => _tagList;
        public string RecordedAt => _recordedAt;
        public string Game => _game;
        public string Preview => _preview;
        public string BroadcastId => _broadcastId;
        public string Url => _url;
        public int Length => _length;
        public int Views => _views;
        public FpsData Fps => _fps;
        public ResolutionsData Resolutions => _resolutions;
        public ChannelData Channel => _channel;

        public TwitchVideo(JToken apiResponse)
        {
            this._title = apiResponse.SelectToken("title").ToString();
            this._description = apiResponse.SelectToken("description").ToString();
            this._status = apiResponse.SelectToken("status").ToString();
            this._id = apiResponse.SelectToken("_id").ToString();
            this._tagList = apiResponse.SelectToken("tag_list").ToString();
            this._recordedAt = apiResponse.SelectToken("recorded_at").ToString();
            this._game = apiResponse.SelectToken("game").ToString();
            this._preview = apiResponse.SelectToken("preview").ToString();
            this._broadcastId = apiResponse.SelectToken("broadcast_id").ToString();
            this._url = apiResponse.SelectToken("url").ToString();
            this._length = int.Parse(apiResponse.SelectToken("length").ToString());
            this._views = int.Parse(apiResponse.SelectToken("views").ToString());
            _fps = new FpsData(int.Parse(apiResponse.SelectToken("fps").SelectToken("audio_only").ToString()),
                double.Parse(apiResponse.SelectToken("fps").SelectToken("medium").ToString()), double.Parse(apiResponse.SelectToken("fps").SelectToken("mobile").ToString()),
                double.Parse(apiResponse.SelectToken("fps").SelectToken("high").ToString()), double.Parse(apiResponse.SelectToken("fps").SelectToken("low").ToString()),
                double.Parse(apiResponse.SelectToken("fps").SelectToken("chunked").ToString()));
            _resolutions = new ResolutionsData(apiResponse.SelectToken("resolutions").SelectToken("medium").ToString(),
                apiResponse.SelectToken("resolutions").SelectToken("mobile").ToString(), apiResponse.SelectToken("resolutions").SelectToken("high").ToString(),
                apiResponse.SelectToken("resolutions").SelectToken("low").ToString(), apiResponse.SelectToken("resolutions").SelectToken("chunked").ToString());
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
                this._audioOnly = audioOnly;
                this._low = low;
                this._medium = medium;
                this._mobile = mobile;
                this._high = high;
                this._chunked = chunked;
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
                this._medium = medium;
                this._mobile = mobile;
                this._high = high;
                this._low = low;
                this._chunked = chunked;
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
                this._name = name;
                this._displayName = displayName;
            }

            public override string ToString()
            {
                return $"{_name}, {_displayName}";
            }
        }
    }
}
