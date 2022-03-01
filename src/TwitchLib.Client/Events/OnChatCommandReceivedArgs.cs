using System;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Events
{
    /// <summary>
    /// Args representing chat command received event.
    /// Implements the <see cref="System.EventArgs" />
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    /// <inheritdoc />
    public class OnChatCommandReceivedArgs : EventArgs
    {
        /// <summary>
        /// The command
        /// </summary>
        /// Property representing received command.
        public ChatCommand Command;
    }
}
