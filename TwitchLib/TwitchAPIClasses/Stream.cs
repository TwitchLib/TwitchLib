using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.TwitchAPIClasses
{
    /// <summary>Class representing a stream as returned by Twitch API</summary>
    public class Stream
    {
        /// <summary>Property representing whether or not the stream is playlist or live.</summary>
        public bool IsPlaylist { get; protected set; }
        /// <summary>Property representing average frames per second.</summary>
        public double AverageFps { get; protected set; }
        /// <summary>Property representing any delay on the stream (in seconds)</summary>
        public int Delay { get; protected set; }
        /// <summary>Property representing height dimension.</summary>
        public int VideoHeight { get; protected set; }
        /// <summary>Property representing number of current viewers.</summary>
        public int Viewers { get; protected set; }
        /// <summary>Property representing the stream id.</summary>
        public long Id { get; protected set; }
        /// <summary>Property representing the preview images in an object.</summary>
        public PreviewObj Preview { get; protected set; }
        /// <summary>Property representing the date time the stream was created.</summary>
        public DateTime CreatedAt { get; protected set; }
        /// <summary>Property representing the current game.</summary>
        public string Game { get; protected set; }
        /// <summary>Property representing the channel the stream is from.</summary>
        public Channel Channel { get; protected set; }

        /// <summary>Stream object constructor.</summary>
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
            CreatedAt = Common.DateTimeStringToObject(twitchStreamData.SelectToken("created_at").ToString());

            Channel = new Channel((JObject) twitchStreamData.SelectToken("channel"));
            Preview = new PreviewObj(twitchStreamData.SelectToken("preview"));
        }

        /// <summary>Class representing the various sizes of previews.</summary>
        public class PreviewObj
        {
            /// <summary>Property representing the small preview size.</summary>
            public string Small { get; protected set; }
            /// <summary>Property representing the medium preview size.</summary>
            public string Medium { get; protected set; }
            /// <summary>Property representing the large preview size.</summary>
            public string Large { get; protected set; }
            /// <summary>Property representing the template preview size.</summary>
            public string Template { get; protected set; }

            /// <summary>PreviewObj object constructor.</summary>
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