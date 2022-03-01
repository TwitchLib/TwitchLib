using System;
using TwitchLib.PubSub.Models.Responses.Messages;

namespace TwitchLib.PubSub.Events
{
    /// <inheritdoc />
    /// <summary>
    /// Class OnChannelSubscriptionArgs.
    /// </summary>
    public class OnChannelSubscriptionArgs : EventArgs
    {
        /// <summary>
        /// The subscription
        /// </summary>
        public ChannelSubscription Subscription;

        /// <summary>
        /// The channel ID the event came from
        /// </summary>
        public string ChannelId;
    }
}
