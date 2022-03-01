using System;

namespace TwitchLib.EventSub.Core.SubscriptionTypes.Channel
{
    /// <summary>
    /// Channel Follow subscription type model
    /// <para>Description:</para>
    /// <para>A specified channel receives a follow.</para>
    /// </summary>
    public class ChannelFollow
    {
        /// <summary>
        /// The user ID for the user now following the specified channel.
        /// </summary>
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// The user display name for the user now following the specified channel.
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// The user login for the user now following the specified channel.
        /// </summary>
        public string UserLogin { get; set; } = string.Empty;
        /// <summary>
        /// The requested broadcaster ID.
        /// </summary>
        public string BroadcasterUserId { get; set; } = string.Empty;
        /// <summary>
        /// The requested broadcaster display name.
        /// </summary>
        public string BroadcasterUserName { get; set; } = string.Empty;
        /// <summary>
        /// The requested broadcaster login.
        /// </summary>
        public string BroadcasterUserLogin { get; set; } = string.Empty;
        /// <summary>
        /// RFC3339 timestamp of when the follow occurred.
        /// </summary>
        public DateTime FollowedAt { get; set; } = DateTime.MinValue;
    }
}