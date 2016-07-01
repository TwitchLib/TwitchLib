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

        /// <summary>Object representing all channel data returned by this request.</summary>
        public ChannelData Channel => _channel;
        /// <summary>Object representing the available FPSs of versions of the video (-1 representings property doesnt exist)</summary>
        public FpsData Fps => _fps;
        /// <summary>Length of video in seconds.</summary>
        public int Length => _length;
        /// <summary>Number of recorded views.</summary>
        public int Views => _views;
        /// <summary>All available resolutions of video.</summary>
        public ResolutionsData Resolutions => _resolutions;
        /// <summary>Unique identifier assigned to broadcast video originated from.</summary>
        public string BroadcastId => _broadcastId;
        /// <summary>Creator's description of video.</summary>
        public string Description => _description;
        /// <summary>Game being played in the video.</summary>
        public string Game => _game;
        /// <summary>Id of the particular video.</summary>
        public string Id => _id;
        /// <summary>Video preview image link.</summary>
        public string Preview => _preview;
        /// <summary>Date and time string representing recorded date.</summary>
        public string RecordedAt => _recordedAt;
        /// <summary>Current status of the recorded video.</summary>
        public string Status => _status;
        /// <summary>Tags assigned to video either automatically or by content creator.</summary>
        public string TagList => _tagList;
        /// <summary>Title of video.</summary>
        public string Title => _title;
        /// <summary>Twitch URL to video.</summary>
        public string Url => _url;

        public TwitchVideo(JToken apiResponse)
        {
            int length = -1;
            int views = -1;
            int audioFps = -1;
            double mediumFps = -1;
            double mobileFps = -1;
            double highFps = -1;
            double lowFps = -1;
            double chunkedFps = -1;

            if (int.TryParse(apiResponse.SelectToken("length").ToString(), out length)) _length = length;
            if (int.TryParse(apiResponse.SelectToken("views").ToString(), out views)) _views = views;

            if(apiResponse.SelectToken("fps").SelectToken("audio_only") != null)
                int.TryParse(apiResponse.SelectToken("fps").SelectToken("audio_only").ToString(), out audioFps);
            if(apiResponse.SelectToken("fps").SelectToken("medium") != null)
                double.TryParse(apiResponse.SelectToken("fps").SelectToken("medium").ToString(), out mediumFps);
            if(apiResponse.SelectToken("fps").SelectToken("mobile") != null)
                double.TryParse(apiResponse.SelectToken("fps").SelectToken("mobile").ToString(), out mobileFps);
            if(apiResponse.SelectToken("fps").SelectToken("high") != null)
                double.TryParse(apiResponse.SelectToken("fps").SelectToken("high").ToString(), out highFps);
            if(apiResponse.SelectToken("fps").SelectToken("low") != null)
                double.TryParse(apiResponse.SelectToken("fps").SelectToken("low").ToString(), out lowFps);
            if(apiResponse.SelectToken("fps").SelectToken("chunked") != null)
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