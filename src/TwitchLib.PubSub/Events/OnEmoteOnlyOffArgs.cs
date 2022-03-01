using System;

namespace TwitchLib.PubSub.Events
{
    /// <inheritdoc />
    /// <summary>
    /// Class representing emotesonly off event.
    /// </summary>
    public class OnEmoteOnlyOffArgs : EventArgs
    {
        /// <summary>
        /// Property representing moderator who issued command.
        /// </summary>
        public string Moderator;
        /// <summary>
        /// The channel ID the event came from
        /// </summary>
        public string ChannelId;
    }
}
