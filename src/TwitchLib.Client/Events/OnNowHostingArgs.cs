using System;

namespace TwitchLib.Client.Events
{
    /// <summary>
    /// Args representing the detected hosted channel.
    /// Implements the <see cref="System.EventArgs" />
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    /// <inheritdoc />
    public class OnNowHostingArgs : EventArgs
    {
        /// <summary>
        /// Property the channel that received the event.
        /// </summary>
        public string Channel;
        /// <summary>
        /// Property representing channel that is being hosted.
        /// </summary>
        public string HostedChannel;
    }
}
