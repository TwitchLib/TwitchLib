namespace TwitchLib.EventSub.Core.SubscriptionTypes.Channel
{
    /// <summary>
    /// Channel Raid subscription type model
    /// <para>Description:</para>
    /// <para>A broadcaster raids another broadcaster’s channel.</para>
    /// </summary>
    public class ChannelRaid
    {
        /// <summary>
        /// The broadcaster ID that created the raid.
        /// </summary>
        public string FromBroadcasterUserId { get; set; } = string.Empty;
        /// <summary>
        /// The broadcaster display name that created the raid.
        /// </summary>
        public string FromBroadcasterUserName { get; set; } = string.Empty;
        /// <summary>
        /// The broadcaster login that created the raid.
        /// </summary>
        public string FromBroadcasterUserLogin { get; set; } = string.Empty;
        /// <summary>
        /// The broadcaster ID that received the raid.
        /// </summary>
        public string ToBroadcasterUserId { get; set; } = string.Empty;
        /// <summary>
        /// The broadcaster display name that received the raid.
        /// </summary>
        public string ToBroadcasterUserName { get; set; } = string.Empty;
        /// <summary>
        /// The broadcaster login that received the raid.
        /// </summary>
        public string ToBroadcasterUserLogin { get; set; } = string.Empty;
        /// <summary>
        /// The number of viewers in the raid.
        /// </summary>
        public int Viewers { get; set; }
    }
}