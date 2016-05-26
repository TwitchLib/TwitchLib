using Newtonsoft.Json.Linq;

namespace TwitchLib
{
    public class TwitchVideo
    {
        public ChannelData Channel { get; }

        public FpsData Fps { get; }

        public int Length { get; }

        public int Views { get; }

        public ResolutionsData Resolutions { get; }

        public string BroadcastId { get; }

        public string Description { get; }

        public string Game { get; }

        public string Id { get; }

        public string Preview { get; }

        public string RecordedAt { get; }

        public string Status { get; }

        public string TagList { get; }

        public string Title { get; }

        public string Url { get; }

        public TwitchVideo(JToken apiResponse)
        {
            int length, views, audioFps;
            double mediumFps, mobileFps, highFps, lowFps, chunkedFps;

            if (int.TryParse(apiResponse.SelectToken("length").ToString(), out length)) Length = length;
            if (int.TryParse(apiResponse.SelectToken("views").ToString(), out views)) Views = views;

            int.TryParse(apiResponse.SelectToken("fps").SelectToken("audio_only").ToString(), out audioFps);
            double.TryParse(apiResponse.SelectToken("fps").SelectToken("medium").ToString(), out mediumFps);
            double.TryParse(apiResponse.SelectToken("fps").SelectToken("mobile").ToString(), out mobileFps);
            double.TryParse(apiResponse.SelectToken("fps").SelectToken("high").ToString(), out highFps);
            double.TryParse(apiResponse.SelectToken("fps").SelectToken("low").ToString(), out lowFps);
            double.TryParse(apiResponse.SelectToken("fps").SelectToken("chunked").ToString(), out chunkedFps);

            BroadcastId = apiResponse.SelectToken("broadcast_id").ToString();
            Description = apiResponse.SelectToken("description").ToString();
            Fps = new FpsData(audioFps, mediumFps, mobileFps, highFps, lowFps, chunkedFps);
            Game = apiResponse.SelectToken("game").ToString();
            Id = apiResponse.SelectToken("_id").ToString();
            Preview = apiResponse.SelectToken("preview").ToString();
            RecordedAt = apiResponse.SelectToken("recorded_at").ToString();
            Status = apiResponse.SelectToken("status").ToString();
            TagList = apiResponse.SelectToken("tag_list").ToString();
            Title = apiResponse.SelectToken("title").ToString();
            Url = apiResponse.SelectToken("url").ToString();

            Resolutions = new ResolutionsData(apiResponse.SelectToken("resolutions").SelectToken("medium").ToString(),
                apiResponse.SelectToken("resolutions").SelectToken("mobile").ToString(),
                apiResponse.SelectToken("resolutions").SelectToken("high").ToString(),
                apiResponse.SelectToken("resolutions").SelectToken("low").ToString(),
                apiResponse.SelectToken("resolutions").SelectToken("chunked").ToString());
            Channel = new ChannelData(apiResponse.SelectToken("channel").SelectToken("name").ToString(),
                apiResponse.SelectToken("channel").SelectToken("display_name").ToString());
        }

        public class FpsData
        {
            public double AudioOnly { get; }

            public double Medium { get; }

            public double Mobile { get; }

            public double High { get; }

            public double Low { get; }

            public double Chunked { get; }

            public FpsData(double audioOnly, double medium, double mobile, double high, double low, double chunked)
            {
                AudioOnly = audioOnly;
                Low = low;
                Medium = medium;
                Mobile = mobile;
                High = high;
                Chunked = chunked;
            }

            public override string ToString()
            {
                return
                    $"audio only: {AudioOnly}, mobile: {Mobile}, low: {Low}, medium: {Medium}, high: {High}, chunked: {Chunked}";
            }
        }

        public class ResolutionsData
        {
            public string Medium { get; }

            public string Mobile { get; }

            public string High { get; }

            public string Low { get; }

            public string Chunked { get; }

            public ResolutionsData(string medium, string mobile, string high, string low, string chunked)
            {
                Medium = medium;
                Mobile = mobile;
                High = high;
                Low = low;
                Chunked = chunked;
            }

            public override string ToString()
            {
                return $"mobile: {Mobile}, low: {Low}, medium: {Medium}, high: {High}, chunked: {Chunked}";
            }
        }

        public class ChannelData
        {
            public string Name { get; }

            public string DisplayName { get; }

            public ChannelData(string name, string displayName)
            {
                Name = name;
                DisplayName = displayName;
            }

            public override string ToString()
            {
                return $"{Name}, {DisplayName}";
            }
        }
    }
}