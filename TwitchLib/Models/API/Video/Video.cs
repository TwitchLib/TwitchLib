using System;
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
        /// <summary>Language code associated with video.</summary>
        public string Language { get; protected set; }
        /// <summary>Length of video in seconds.</summary>
        public TimeSpan Length { get; protected set; }
        /// <summary>Number of recorded views.</summary>
        public int Views { get; protected set; }
        /// <summary>All available resolutions of video.</summary>
        public Resolutions Resolutions { get; protected set; }
        /// <summary>Unique identifier assigned to broadcast video originated from.</summary>
        public string BroadcastId { get; protected set; }
        /// <summary>Represents teh type of broadcast (could be custom upload)</summary>
        public string BroadcastType { get; protected set; }
        /// <summary>Creator's description of video.</summary>
        public string Description { get; protected set; }
        /// <summary>Similar to Description property, but with HTML elements.</summary>
        public string DescriptionHtml { get; protected set; }
        /// <summary>Game being played in the video.</summary>
        public string Game { get; protected set; }
        /// <summary>Id of the particular video.</summary>
        public string Id { get; protected set; }
        /// <summary>Video preview image link.</summary>
        public Preview Preview { get; protected set; }
        /// <summary>Date and time string representing recorded date.</summary>
        public DateTime CreatedAt { get; protected set; }
        /// <summary>TimeSpan object representing the time since the video was recorded.</summary>
        public TimeSpan TimeSinceCreated { get; protected set; }
        /// <summary>DateTime object representing the period at which the video was published.</summary>
        public DateTime PublishedAt { get; protected set; }
        /// <summary>Current status of the recorded video.</summary>
        public string Status { get; protected set; }
        /// <summary>Tags assigned to video either automatically or by content creator.</summary>
        public string TagList { get; protected set; }
        /// <summary>Title of video.</summary>
        public string Title { get; protected set; }
        /// <summary>Twitch URL to video.</summary>
        public string Url { get; protected set; }
        /// <summary>Current view status.</summary>
        public string Viewable { get; protected set; }
        /// <summary>View location.</summary>
        public string ViewableAt { get; protected set; }

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

            if (int.TryParse(apiResponse.SelectToken("length").ToString(), out length)) Length = TimeSpan.FromSeconds(length);
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

            BroadcastId = apiResponse.SelectToken("broadcast_id")?.ToString();
            BroadcastType = apiResponse.SelectToken("broadcast_type")?.ToString();
            Description = apiResponse.SelectToken("description")?.ToString();
            DescriptionHtml = apiResponse.SelectToken("description_html")?.ToString();
            if(apiResponse.SelectToken("fps") != null)
                Fps = new FPS(apiResponse.SelectToken("fps"));
            Game = apiResponse.SelectToken("game")?.ToString();
            Id = apiResponse.SelectToken("_id")?.ToString();
            if (apiResponse.SelectToken("preview") != null)
                Preview = new Preview(apiResponse.SelectToken("preview"));
            CreatedAt = Common.Helpers.DateTimeStringToObject(apiResponse.SelectToken("created_at").ToString());
            TimeSinceCreated = DateTime.UtcNow - CreatedAt;
            Status = apiResponse.SelectToken("status")?.ToString();
            TagList = apiResponse.SelectToken("tag_list")?.ToString();
            Title = apiResponse.SelectToken("title")?.ToString();
            Url = apiResponse.SelectToken("url")?.ToString();
            if (apiResponse.SelectToken("published_at") != null)
                PublishedAt = Common.Helpers.DateTimeStringToObject(apiResponse.SelectToken("published_at").ToString());
            if(apiResponse.SelectToken("resolutions") != null)
                Resolutions = new Resolutions(apiResponse.SelectToken("resolutions"));
            Channel = new Channel(apiResponse.SelectToken("channel"));
            Viewable = apiResponse.SelectToken("viewable")?.ToString();
            ViewableAt = apiResponse.SelectToken("viewable_at")?.ToString();
        }
    }
}