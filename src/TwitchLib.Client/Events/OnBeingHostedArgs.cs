using System;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Events
{
    /// <summary>
    /// Args representing an event where another channel has started hosting the broadcaster's channel.
    /// Implements the <see cref="System.EventArgs" />
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    /// <inheritdoc />
    public class OnBeingHostedArgs : EventArgs
    {
        /// <summary>
        /// Property representing the Host notification
        /// </summary>
        public BeingHostedNotification BeingHostedNotification;
    }
}
