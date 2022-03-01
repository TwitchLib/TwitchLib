namespace TwitchLib.EventSub.Webhooks.Core.SubscriptionTypes.Stream
{
    /// <summary>
    /// Stream Offline subscription type model
    /// <para>Description:</para>
    /// <para>The specified broadcaster stops a stream.</para>
    /// </summary>
    public class StreamOffline
    {
        /// <summary>
        /// The broadcaster's user id.
        /// </summary>
        public string BroadcasterUserId { get; set; } = string.Empty;
        /// <summary>
        /// The broadcaster's user display name.
        /// </summary>
        public string BroadcasterUserName { get; set; } = string.Empty;
        /// <summary>
        /// The broadcaster's user login.
        /// </summary>
        public string BroadcasterUserLogin { get; set; } = string.Empty;
    }
}