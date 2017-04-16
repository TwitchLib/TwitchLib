namespace TwitchLib.Models.API.v5.Chat
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    /// <summary>Class representing the channel badges response from Twitch API.</summary>
    public class ChannelBadges
    {
        #region Admin
        /// <summary>Property representing the admin badge.</summary>
        [JsonProperty(PropertyName = "admin")]
        public Badge Admin { get; protected set; }
        #endregion
        #region Broadcaster
        /// <summary>Property representing the broadcaster badge.</summary>
        [JsonProperty(PropertyName = "broadcaster")]
        public Badge Broadcaster { get; protected set; }
        #endregion
        #region GlobalMod
        /// <summary>Property representing the global moderator badge.</summary>
        [JsonProperty(PropertyName = "global_mod")]
        public Badge GlobalMod { get; protected set; }
        #endregion
        #region Mod
        /// <summary>Property representing the moderator badge.</summary>
        [JsonProperty(PropertyName = "mod")]
        public Badge Mod { get; protected set; }
        #endregion
        #region Staff
        /// <summary>Property representing the staff badge.</summary>
        [JsonProperty(PropertyName = "staff")]
        public Badge Staff { get; protected set; }
        #endregion
        #region Subscriber TODO: Check if they finally deliver all sub badges
        /// <summary>Property representing the subscriber badge(s).</summary>
        [JsonProperty(PropertyName = "subscriber")]
        public Badge Subscriber { get; protected set; }
        #endregion
        #region Turbo
        /// <summary>Property representing the turbo badge.</summary>
        [JsonProperty(PropertyName = "turbo")]
        public Badge Turbo { get; protected set; }
        #endregion
        #region Prime TODO: Check if they finally deliver the prime badge at all
        /// <summary>Property representing the prime badge.</summary>
        [JsonProperty(PropertyName = "premium")]
        public Badge Premium { get; protected set; }
        #endregion
    }
}
