using System;

namespace TwitchLib.PubSub.Events
{
    /// <inheritdoc />
    /// <summary>
    /// Class representing arguments of chat clear event.
    /// </summary>
    public class OnClearArgs : EventArgs
    {
        /// <summary>
        /// Property representing username of moderator who cleared chat.
        /// </summary>
        public string Moderator;
        /// <summary>
        /// The channel ID the event came from
        /// </summary>
        public string ChannelId;
    }
}
