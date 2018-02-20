using System;
using TwitchLib.Client.Models.Client;

namespace TwitchLib.Client.Events.Client
{
    /// <inheritdoc />
    /// <summary>Args representing chat command received event.</summary>
    public class OnChatCommandReceivedArgs : EventArgs
    {
        /// Property representing received command.
        public ChatCommand Command;
    }
}
