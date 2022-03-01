using System;
using System.Collections.Generic;

namespace TwitchLib.PubSub.Events
{
    /// <inheritdoc />
    /// <summary>
    /// Class OnChannelExtensionBroadcastArgs.
    /// </summary>
    public class OnChannelExtensionBroadcastArgs : EventArgs
    {
        /// <summary>
        /// Property containing the payload send to the specified extension on the specified channel.
        /// </summary>
        public List<string> Messages;
        /// <summary>
        /// The channel ID the event came from
        /// </summary>
        public string ChannelId;
    }
}
