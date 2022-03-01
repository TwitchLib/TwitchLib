using System;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Events
{
    /// <summary>
    /// Class OnContinuedGiftedSubscriptionArgs.
    /// Implements the <see cref="System.EventArgs" />
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class OnContinuedGiftedSubscriptionArgs : EventArgs
    {
        /// <summary>
        /// Property representing the information of the subscription that was originally gifted, and is now continued by the user.
        /// </summary>
        public ContinuedGiftedSubscription ContinuedGiftedSubscription;
        /// <summary>
        /// Property representing the Twitch channel this event fired from.
        /// </summary>
        public string Channel;
    }
}