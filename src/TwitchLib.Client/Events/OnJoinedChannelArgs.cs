using System;

namespace TwitchLib.Client.Events
{
    /// <summary>
    /// Args representing on channel joined event.
    /// Implements the <see cref="System.EventArgs" />
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    /// <inheritdoc />
    public class OnJoinedChannelArgs : EventArgs
    {
        /// <summary>
        /// Property representing bot username.
        /// </summary>
        public string BotUsername;
        /// <summary>
        /// Property representing the channel that was joined.
        /// </summary>
        public string Channel;
    }
}
