using System;

namespace TwitchLib.Client.Events
{
    /// <summary>
    /// Args representing a NOTICE telling the client that a verified phone number is required to chat.
    /// Implements the <see cref="System.EventArgs" />
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    /// <inheritdoc />
    public class OnRequiresVerifiedPhoneNumberArgs : EventArgs
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