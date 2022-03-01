using System;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Events
{
    /// <summary>
    /// Class OnRaidNotificationArgs.
    /// Implements the <see cref="System.EventArgs" />
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class OnRaidNotificationArgs : EventArgs
    {
        /// <summary>
        /// The raid notification
        /// </summary>
        public RaidNotification RaidNotification;
        /// <summary>
        /// The channel
        /// </summary>
        public string Channel;
    }
}
