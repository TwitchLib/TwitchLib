using Newtonsoft.Json;

namespace TwitchLib.Models.API.v5.Channels
{
    /// <summary>Class representing a commercial object from Twitch API.</summary>
    public class ChannelCommercial
    {
        #region Duration
        /// <summary>Property representing the duration of the commercial.</summary>
        [JsonProperty(PropertyName = "duration")]
        public int Duration { get; protected set; }
        #endregion
        #region Message
        /// <summary>Property representing the commercial message.</summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; protected set; }
        #endregion
        #region Retry after
        /// <summary>Property representing the retryafter response.</summary>
        [JsonProperty(PropertyName = "retryafter")]
        public int RetryAfter { get; protected set; }
        #endregion
    }
}
