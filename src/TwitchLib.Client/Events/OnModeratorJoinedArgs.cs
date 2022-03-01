using System;

namespace TwitchLib.Client.Events
{
    /// <summary>
    /// Args representing moderator joined event.
    /// Implements the <see cref="System.EventArgs" />
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    /// <inheritdoc />
    public class OnModeratorJoinedArgs : EventArgs
    {
        /// <summary>
        /// Property representing username of joined moderator.
        /// </summary>
        public string Username;
        /// <summary>
        /// Property representing channel bot is connected to.
        /// </summary>
        public string Channel;
    }
}
