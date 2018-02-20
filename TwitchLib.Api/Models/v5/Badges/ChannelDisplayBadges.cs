using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v5.Badges
{
    public class ChannelDisplayBadges
    {
        #region BadgeSets
        [JsonProperty(PropertyName = "badge_sets")]
        public BadgeSets Sets { get; protected set; }
        #endregion
    }

    public class BadgeSets
    {
        #region Subscriber
        [JsonProperty(PropertyName = "subscriber")]
        public Badge Subscriber { get; protected set; }
        #endregion

        #region Custom Bit Badges
        [JsonProperty(PropertyName = "bits")]
        public Badge Bits { get; protected set; }
        #endregion
    }
}
