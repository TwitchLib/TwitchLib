using System;

namespace TwitchLib.PubSub.Events
{
    /// <inheritdoc />
    /// <summary>
    /// Class representing event where r9k was enabled
    /// </summary>
    public class OnR9kBetaArgs : EventArgs
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
