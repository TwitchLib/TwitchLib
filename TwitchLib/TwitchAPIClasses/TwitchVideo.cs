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

        public string Title { get { return _title; } }
        public string Description { get { return _description; } }
        public string Status { get { return _status; } }
        public string Id { get { return _id; } }
        public string TagList { get { return _tagList; } }
        public string RecordedAt { get { return _recordedAt; } }
        public string Game { get { return _game; } }
        public string Preview { get { return _preview; } }
        public string BroadcastId { get { return _broadcastId; } }
        public string Url { get { return _url; } }
        public int Length { get { return _length; } }
        public int Views { get { return _views; } }
        public FpsData Fps { get { return _fps; } }
        public ResolutionsData Resolutions { get { return _resolutions; } }
        public ChannelData Channel { get { return _channel; } }

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

            public double AudioOnly { get { return _audioOnly; } }
            public double Medium { get { return _medium; } }
            public double Mobile { get { return _mobile; } }
            public double High { get { return _high; } }
            public double Low { get { return _low; } }
            public double Chunked { get { return _chunked; } }

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
                return string.Format("audio only: {0}, mobile: {1}, low: {2}, medium: {3}, high: {4}, chunked: {5}",
                    _audioOnly, _mobile, _low, _medium, _high, _chunked);
            }
        }

        public class ResolutionsData
        {
            string _medium, _mobile, _high, _low, _chunked;

            public string Medium { get { return _medium; } }
            public string Mobile { get { return _mobile; } }
            public string High { get { return _high; } }
            public string Low { get { return _low; } }
            public string Chunked { get { return _chunked; } }

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
                return string.Format("mobile: {0}, low: {1}, medium: {2}, high: {3}, chunked: {4}", _mobile,
                    _low, _medium, _high, _chunked);
            }
        }

        public class ChannelData
        {
            string _name, _displayName;

            public string Name { get { return _name; } }
            public string DisplayName { get { return _displayName; } }

            public ChannelData(string name, string displayName)
            {
                this._name = name;
                this._displayName = displayName;
            }

            public override string ToString()
            {
                return string.Format("{0}, {1}", _name, _displayName);
            }
        }
    }
}
