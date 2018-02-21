using System;

namespace TwitchLib.Client.Events.Client
{
    /// <inheritdoc />
    /// <summary>Args representing moderator leave event.</summary>
    public class OnModeratorLeftArgs : EventArgs
    {
        /// <summary>Property representing username of moderator that left..</summary>
        public string Username;
        /// <summary>Property representing channel bot is connected to.</summary>
        public string Channel;
    }
}
