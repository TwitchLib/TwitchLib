namespace TwitchLib.Models.API.v5.Streams
{
    #region using directives
    using Newtonsoft.Json;
    using System;
    #endregion
    public class Stream
    {
        #region Id
        /// <summary>Property representing the Stream ID.</summary>
        [JsonProperty(PropertyName = "_id")]
        public long Id { get; protected set; }
        #endregion
        #region AverageFps
        /// <summary>Property representing the average fps count of the stream.</summary>
        [JsonProperty(PropertyName = "average_fps")]
        public double AverageFps { get; protected set; }
        #endregion
        #region Channel
        /// <summary>Property representing the channel ID.</summary>
        [JsonProperty(PropertyName = "channel")]
        public Channels.Channel Channel { get; protected set; }
        #endregion
        #region CreatedAt
        /// <summary>Property representing the date time of channel creation.</summary>
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; protected set; }
        #endregion
        #region Delay
        /// <summary>Property representing the Stream delay.</summary>
        [JsonProperty(PropertyName = "delay")]
        public int Delay { get; protected set; }
        #endregion
        #region Game
        /// <summary>Property representing the Stream game.</summary>
        [JsonProperty(PropertyName = "game")]
        public string Game { get; protected set; }
        #endregion
        #region IsPlaylist
        /// <summary>Property representing wether the Stream is a Playlist or not.</summary>
        [JsonProperty(PropertyName = "is_playlist")]
        public bool IsPlaylist { get; protected set; }
        #endregion
        #region StreamType
        /// <summary>Property representing the type of stream (live, watch_party, etc)</summary>
        [JsonProperty(PropertyName = "stream_type")]
        public string StreamType { get; protected set; }
        #endregion
        #region Preview
        /// <summary>Property representing wether the Stream is a Playlist or not.</summary>
        [JsonProperty(PropertyName = "preview")]
        public StreamPreview Preview { get; protected set; }
        #endregion
        #region VideoHeight
        /// <summary>Property representing the Stream video height.</summary>
        [JsonProperty(PropertyName = "video_height")]
        public int VideoHeight { get; protected set; }
        #endregion
        #region Viewers
        /// <summary>Property representing the Stream viewer count.</summary>
        [JsonProperty(PropertyName = "viewers")]
        public int Viewers { get; protected set; }
        #endregion
    }
}
