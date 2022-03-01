using System;

namespace TwitchLib.Client.Events
{
    /// <summary>
    /// Class OnLogArgs.
    /// Implements the <see cref="System.EventArgs" />
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class OnLogArgs : EventArgs
    {
        /// <summary>
        /// The bot username
        /// </summary>
        public string BotUsername;
        /// <summary>
        /// The data
        /// </summary>
        public string Data;
        /// <summary>
        /// The date time
        /// </summary>
        public DateTime DateTime;
    }
}
