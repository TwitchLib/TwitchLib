using System;

namespace TwitchLib.PubSub.Events
{
    /// <inheritdoc />
    /// <summary>
    /// Class representing arguments of on host event.
    /// </summary>
    public class OnHostArgs : EventArgs
    {
        /// <summary>
        /// Property representing moderator who issued command.
        /// </summary>
        public string Moderator;
        /// <summary>
        /// Property representing hosted channel.
        /// </summary>
        public string HostedChannel;
        /// <summary>
        /// The channel ID the event came from
        /// </summary>
        public string ChannelId;
    }
}
