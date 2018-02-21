using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v5.Channels
{
    /// <summary>
    /// Class representing a privileged channel object from Twitch API.
    /// This class contains more data than the Channel class because the channel object is based on a specified oauth token.
    /// </summary>
    public class ChannelAuthed
    {
        #region Id
        /// <summary>Property representing the channel ID.</summary>
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; protected set; }
        #endregion
        #region BroadcasterLanguage
        /// <summary>Property representing the broadcasters language.</summary>
        [JsonProperty(PropertyName = "broadcaster_language")]
        public string BroadcasterLanguage { get; protected set; }
        #endregion
        #region BroadcasterType
        [JsonProperty(PropertyName = "broadcaster_type")]
        public string BroadcasterType { get; protected set; }
        #endregion
        #region CreatedAt
        /// <summary>Property representing the date time of channel creation.</summary>
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; protected set; }
        #endregion
        #region DisplayName
        /// <summary>Property representing the case sensitive display name of the channel.</summary>
        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; protected set; }
        #endregion
        #region Email
        /// <summary>Property representing the registration email address of the channel.</summary>
        [JsonProperty(PropertyName = "email")]
        public string Email { get; protected set; }
        #endregion
        #region Followers
        /// <summary>Property representing the followers count of the channel.</summary>
        [JsonProperty(PropertyName = "followers")]
        public int Followers { get; protected set; }
        #endregion
        #region Game
        /// <summary>Property representing the currently played game.</summary>
        [JsonProperty(PropertyName = "game")]
        public string Game { get; protected set; }
        #endregion
        #region Language
        /// <summary>Property representing the signed language.</summary>
        [JsonProperty(PropertyName = "language")]
        public string Language { get; protected set; }
        #endregion
        #region Logo
        /// <summary>Property representing the logo of the channel</summary>
        [JsonProperty(PropertyName = "logo")]
        public string Logo { get; protected set; }
        #endregion
        #region Mature
        /// <summary>Property representing wether the channel is for mature audience or not.</summary>
        [JsonProperty(PropertyName = "mature")]
        public bool Mature { get; protected set; }
        #endregion
        #region Name
        /// <summary>Property representing the name of the channel (always in lowercase).</summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; protected set; }
        #endregion
        #region Partner
        /// <summary>Property representing wether the channel is partnered or not.</summary>
        [JsonProperty(PropertyName = "partner")]
        public bool Partner { get; protected set; }
        #endregion
        #region ProfileBanner
        /// <summary>Property representing the profile banner of the channel.</summary>
        [JsonProperty(PropertyName = "profile_banner")]
        public string ProfileBanner { get; protected set; }
        #endregion
        #region ProfileBannerBackgroundColor
        /// <summary>Property representing the profile banner background color of the channel.</summary>
        [JsonProperty(PropertyName = "profile_banner_background_color")]
        public string ProfileBannerBackgroundColor { get; protected set; }
        #endregion
        #region Status
        /// <summary>Property representing the status of the channel.</summary>
        [JsonProperty(PropertyName = "status")]
        public string Status { get; protected set; }
        #endregion
        #region Stream Key
        /// <summary>Property representing the stream key of the channel.</summary>
        [JsonProperty(PropertyName = "stream_key")]
        public string StreamKey { get; protected set; }
        #endregion
        #region UpdatedAt
        /// <summary>Property representing the date time of last channel update.</summary>
        [JsonProperty(PropertyName = "updated_at")]
        public DateTime UpdatedAt { get; protected set; }
        #endregion
        #region Url
        /// <summary>Property representing the url to the channel.</summary>
        [JsonProperty(PropertyName = "url")]
        public string Url { get; protected set; }
        #endregion
        #region VideoBanner
        /// <summary>Property representing the video banner of the channel.</summary>
        [JsonProperty(PropertyName = "video_banner")]
        public string VideoBanner { get; protected set; }
        #endregion
        #region Views
        /// <summary>Property representing the number of views the channel has.</summary>
        [JsonProperty(PropertyName = "views")]
        public int Views { get; protected set; }
        #endregion
    }
}
