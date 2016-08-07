using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.TwitchAPIClasses
{
    /// <summary>
    /// Class representing returned Video object.
    /// </summary>
    public class Video
    {
        /// <summary>Object representing all channel data returned by this request.</summary>
        public ChannelData Channel { get; protected set; }
        /// <summary>Object representing the available FPSs of versions of the video (-1 representings property doesnt exist)</summary>
        public FpsData Fps { get; protected set; }
        /// <summary>Length of video in seconds.</summary>
        public int Length { get; protected set; }
        /// <summary>Number of recorded views.</summary>
        public int Views { get; protected set; }
        /// <summary>All available resolutions of video.</summary>
        public ResolutionsData Resolutions { get; protected set; }
        /// <summary>Unique identifier assigned to broadcast video originated from.</summary>
        public string BroadcastId { get; protected set; }
        /// <summary>Creator's description of video.</summary>
        public string Description { get; protected set; }
        /// <summary>Game being played in the video.</summary>
        public string Game { get; protected set; }
        /// <summary>Id of the particular video.</summary>
        public string Id { get; protected set; }
        /// <summary>Video preview image link.</summary>
        public string Preview { get; protected set; }
        /// <summary>Date and time string representing recorded date.</summary>
        public string RecordedAt { get; protected set; }
        /// <summary>Current status of the recorded video.</summary>
        public string Status { get; protected set; }
        /// <summary>Tags assigned to video either automatically or by content creator.</summary>
        public string TagList { get; protected set; }
        /// <summary>Title of video.</summary>
        public string Title { get; protected set; }
        /// <summary>Twitch URL to video.</summary>
        public string Url { get; protected set; }

        /// <summary>
        /// Video constructor
        /// </summary>
        /// <param name="apiResponse">API response string from Twitch call.</param>
        public Video(JToken apiResponse)
        {
            int length = -1;
            int views = -1;
            int audioFps = -1;
            double mediumFps = -1;
            double mobileFps = -1;
            double highFps = -1;
            double lowFps = -1;
            double chunkedFps = -1;

            if (int.TryParse(apiResponse.SelectToken("length").ToString(), out length)) Length = length;
            if (int.TryParse(apiResponse.SelectToken("views").ToString(), out views)) Views = views;

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
        /// <summary>Class representing FPS data.</summary>
        public class FpsData
        {
            /// <summary>Property representing FPS for audio only.</summary>
            public double AudioOnly { get; protected set; }
            /// <summary>Property representing FPS for medium quality.</summary>
            public double Medium { get; protected set; }
            /// <summary>Property representing FPS for mobile quality.</summary>
            public double Mobile { get; protected set; }
            /// <summary>Property representing FPS for high quality.</summary>
            public double High { get; protected set; }
            /// <summary>Property representing FPS for low quality.</summary>
            public double Low { get; protected set; }
            /// <summary>Property representing FPS for chunked quality.</summary>
            public double Chunked { get; protected set; }

            /// <summary>
            /// FPS Data constructor.
            /// </summary>
            /// <param name="audioOnly"></param>
            /// <param name="medium"></param>
            /// <param name="mobile"></param>
            /// <param name="high"></param>
            /// <param name="low"></param>
            /// <param name="chunked"></param>
            public FpsData(double audioOnly, double medium, double mobile, double high, double low, double chunked)
            {
                AudioOnly = audioOnly;
                Low = low;
                Medium = medium;
                Mobile = mobile;
                High = high;
                Chunked = chunked;
            }

            /// <summary>Returns string in format: audio only: {}, mobile: {} etc.</summary>
            public override string ToString()
            {
                return
                    $"audio only: {AudioOnly}, mobile: {Mobile}, low: {Low}, medium: {Medium}, high: {High}, chunked: {Chunked}";
            }
        }

        /// <summary>Class representing resolution data.</summary>
        public class ResolutionsData
        {
            /// <summary>Property representing relation for medium quality.</summary>
            public string Medium { get; protected set; }
            /// <summary>Property representing relation for mobile quality.</summary>
            public string Mobile { get; protected set; }
            /// <summary>Property representing relation for high quality.</summary>
            public string High { get; protected set; }
            /// <summary>Property representing relation for low quality.</summary>
            public string Low { get; protected set; }
            /// <summary>Property representing relation for chunked quality.</summary>
            public string Chunked { get; protected set; }

            /// <summary>
            /// Resolutions data constructor
            /// </summary>
            /// <param name="medium"></param>
            /// <param name="mobile"></param>
            /// <param name="high"></param>
            /// <param name="low"></param>
            /// <param name="chunked"></param>
            public ResolutionsData(string medium, string mobile, string high, string low, string chunked)
            {
                Medium = medium;
                Mobile = mobile;
                High = high;
                Low = low;
                Chunked = chunked;
            }

            /// <summary>Returns string in format: mobile: {}, low: {} etc</summary>
            public override string ToString()
            {
                return $"mobile: {Mobile}, low: {Low}, medium: {Medium}, high: {High}, chunked: {Chunked}";
            }
        }

        /// <summary>Class representing channel data.</summary>
        public class ChannelData
        {
            /// <summary>Property representing Name of channel.</summary>
            public string Name { get; protected set; }
            /// <summary>Property representing DisplayName of channel.</summary>
            public string DisplayName { get; protected set; }

            /// <summary>Channel data construcotr.</summary>
            public ChannelData(string name, string displayName)
            {
                Name = name;
                DisplayName = displayName;
            }

            /// <summary>Returns string in format: {name}, {displayname}</summary>
            public override string ToString()
            {
                return $"{Name}, {DisplayName}";
            }
        }
    }
}