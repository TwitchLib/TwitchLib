using System;

namespace TwitchLib.Events.Client
{
    /// <inheritdoc />
    /// <summary>Args representing on connected event.</summary>
    public class OnConnectedArgs : EventArgs
    {
        /// <summary>Property representing bot username.</summary>
        public string BotUsername;
        /// <summary>Property representing connected channel.</summary>
        public string AutoJoinChannel;
    }
}
