using System;

namespace TwitchLib.PubSub.Events
{
    /// <inheritdoc />
    /// <summary>
    /// Class representing subscriber only mode event starting.
    /// </summary>
    public class OnSubscribersOnlyArgs : EventArgs
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
