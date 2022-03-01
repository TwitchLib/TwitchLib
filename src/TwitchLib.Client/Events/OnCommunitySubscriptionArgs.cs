using System;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Events
{
    /// <summary>
    /// Args representing community subscription received event.
    /// Implements the <see cref="System.EventArgs" />
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    /// <inheritdoc />
    public class OnCommunitySubscriptionArgs : EventArgs
    {
        /// <summary>
        /// Property representing the information of the community subscription.
        /// </summary>
        public CommunitySubscription GiftedSubscription;
        /// <summary>
        /// Property representing the Twitch channel this event fired from.
        /// </summary>
        public string Channel;
    }
}
