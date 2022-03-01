using System;

namespace TwitchLib.Client.Events
{
    /// <summary>
    /// Args representing a NOTICE telling the client that the user is banned to chat bcs of an already banned alias with the same Email.
    /// Implements the <see cref="System.EventArgs" />
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    /// <inheritdoc />
    public class OnBannedEmailAliasArgs : EventArgs
    {
        /// <summary>
        /// Property representing message send with the NOTICE
        /// </summary>
        public string Message;
        /// <summary>
        /// Property representing channel bot is connected to.
        /// </summary>
        public string Channel;
    }
}