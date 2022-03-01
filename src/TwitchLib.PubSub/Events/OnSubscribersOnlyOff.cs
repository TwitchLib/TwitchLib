using System;

namespace TwitchLib.PubSub.Events
{
    /// <inheritdoc />
    /// <summary>
    /// Class representing subscriber only mode turning off event.
    /// </summary>
    public class OnSubscribersOnlyOffArgs : EventArgs
    {
        /// <summary>
        /// Property representing the moderator that issued the command.
        /// </summary>
        public string Moderator;
        /// <summary>
        /// The channel ID the event came from
        /// </summary>
        public string ChannelId;
    }
}
