using System;

namespace TwitchLib.Client.Events
{
    /// <summary>
    /// Args representing moderator leave event.
    /// Implements the <see cref="System.EventArgs" />
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    /// <inheritdoc />
    public class OnModeratorLeftArgs : EventArgs
    {
        /// <summary>
        /// Property representing username of moderator that left..
        /// </summary>
        public string Username;
        /// <summary>
        /// Property representing channel bot is connected to.
        /// </summary>
        public string Channel;
    }
}
