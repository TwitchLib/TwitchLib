using System;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Events
{
    /// <summary>
    /// Args representing client connection error event.
    /// Implements the <see cref="System.EventArgs" />
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    /// <inheritdoc />
    public class OnConnectionErrorArgs : EventArgs
    {
        /// <summary>
        /// The error
        /// </summary>
        public ErrorEvent Error;
        /// <summary>
        /// Username of the bot that suffered connection error.
        /// </summary>
        public string BotUsername;
    }
}
