using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.Models.API.Video
{
    /// <summary>
    /// Class representing returned Video object.
    /// </summary>
    public class Video
    {
        /// <summary>Object representing all channel data returned by this request.</summary>
        public Channel Channel { get; protected set; }
        /// <summary>Object representing the available FPSs of versions of the video (-1 representings property doesnt exist)</summary>
        public FPS Fps { get; protected set; }
        /// <summary>Length of video in seconds.</summary>
        public int Length { get; protected set; }
        /// <summary>Number of recorded views.</summary>
        public int Views { get; protected set; }
        /// <summary>All available resolutions of video.</summary>
        public Resolution Resolutions { get; protected set; }
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
        public DateTime RecordedAt { get; protected set; }
        /// <summary>TimeSpan object representing the time since the video was recorded.</summary>
        public TimeSpan TimeSinceRecorded { get; protected set; }
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
            Fps = new FPS(audioFps, mediumFps, mobileFps, highFps, lowFps, chunkedFps);
            Game = apiResponse.SelectToken("game").ToString();
            Id = apiResponse.SelectToken("_id").ToString();
            Preview = apiResponse.SelectToken("preview").ToString();
            RecordedAt = Common.DateTimeStringToObject(apiResponse.SelectToken("recorded_at").ToString());
            TimeSinceRecorded = DateTime.UtcNow - RecordedAt;
            Status = apiResponse.SelectToken("status").ToString();
            TagList = apiResponse.SelectToken("tag_list").ToString();
            Title = apiResponse.SelectToken("title").ToString();
            Url = apiResponse.SelectToken("url").ToString();

            Resolutions = new Resolution(apiResponse.SelectToken("resolutions").SelectToken("medium").ToString(),
                apiResponse.SelectToken("resolutions").SelectToken("mobile").ToString(),
                apiResponse.SelectToken("resolutions").SelectToken("high").ToString(),
                apiResponse.SelectToken("resolutions").SelectToken("low").ToString(),
                apiResponse.SelectToken("resolutions").SelectToken("chunked").ToString());
            Channel = new Channel(apiResponse.SelectToken("channel").SelectToken("name").ToString(),
                apiResponse.SelectToken("channel").SelectToken("display_name").ToString());
        }
    }
}