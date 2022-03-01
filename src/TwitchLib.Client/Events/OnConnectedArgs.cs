using System;

namespace TwitchLib.Client.Events
{
    /// <summary>
    /// Args representing on connected event.
    /// Implements the <see cref="System.EventArgs" />
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    /// <inheritdoc />
    public class OnConnectedArgs : EventArgs
    {
        /// <summary>
        /// Property representing bot username.
        /// </summary>
        public string BotUsername;
        /// <summary>
        /// Property representing connected channel.
        /// </summary>
        public string AutoJoinChannel;
    }
}
