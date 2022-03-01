using System;

namespace TwitchLib.Client.Events
{
    /// <summary>
    /// Class OnUnaccountedForArgs.
    /// Implements the <see cref="System.EventArgs" />
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class OnUnaccountedForArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the raw irc.
        /// </summary>
        /// <value>The raw irc.</value>
        public string RawIRC { get; set; }
        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>The location.</value>
        public string Location { get; set; }
        /// <summary>
        /// Gets or sets the bot username.
        /// </summary>
        /// <value>The bot username.</value>
        public string BotUsername { get; set; } // may not be available
        /// <summary>
        /// Gets or sets the channel.
        /// </summary>
        /// <value>The channel.</value>
        public string Channel { get; set; } // may not be available
    }
}
