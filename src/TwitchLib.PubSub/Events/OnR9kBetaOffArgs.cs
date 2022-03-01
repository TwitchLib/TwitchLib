using System;

namespace TwitchLib.PubSub.Events
{
    /// <inheritdoc />
    /// <summary>
    /// Class representing event where r9k was turned off.
    /// </summary>
    public class OnR9kBetaOffArgs : EventArgs
    {
        /// <summary>
        /// Property representing moderator that issued command.
        /// </summary>
        public string Moderator;
        /// <summary>
        /// The channel ID the event came from
        /// </summary>
        public string ChannelId;
    }
}
