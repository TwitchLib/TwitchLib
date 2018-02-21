using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v5.Channels
{
    /// <summary>Class representing the channel subscribers response from Twitch API.</summary>
    public class ChannelSubscribers
    {
        #region Total
        /// <summary>Property representing the subscriber count of the channel.</summary>
        [JsonProperty(PropertyName = "_total")]
        public int Total { get; protected set; }
        #endregion
        #region Subscriptions
        /// <summary>Property representing the subscriptions collection of the channel.</summary>
        [JsonProperty(PropertyName = "subscriptions")]
        public Subscriptions.Subscription[] Subscriptions { get; protected set; }
        #endregion
    }
}
