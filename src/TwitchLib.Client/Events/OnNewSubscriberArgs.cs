using System;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Events
{
    /// <summary>
    /// Args representing new subscriber event.
    /// Implements the <see cref="System.EventArgs" />
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    /// <inheritdoc />
    public class OnNewSubscriberArgs : EventArgs
    {
        /// <summary>
        /// Property representing subscriber object.
        /// </summary>
        public Subscriber Subscriber;
        /// <summary>
        /// Property representing the Twitch channel this event fired from.
        /// </summary>
        public string Channel;
    }
}
