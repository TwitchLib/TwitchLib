using System;

namespace TwitchLib.Client.Events
{
    /// <summary>
    /// Args representing viewer joined event.
    /// Implements the <see cref="System.EventArgs" />
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    /// <inheritdoc />
    public class OnUserJoinedArgs : EventArgs
    {
        /// <summary>
        /// Property representing username of joined viewer.
        /// </summary>
        public string Username;
        /// <summary>
        /// Property representing channel bot is connected to.
        /// </summary>
        public string Channel;
    }
}
