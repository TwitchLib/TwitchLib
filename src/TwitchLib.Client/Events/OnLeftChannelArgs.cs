using System;

namespace TwitchLib.Client.Events
{
    /// <summary>
    /// Args representing the client left a channel event.
    /// Implements the <see cref="System.EventArgs" />
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    /// <inheritdoc />
    public class OnLeftChannelArgs : EventArgs
    {
        /// <summary>
        /// The username of the bot that left the channel.
        /// </summary>
        public string BotUsername;
        /// <summary>
        /// Channel that bot just left (parted).
        /// </summary>
        public string Channel;
    }
}
