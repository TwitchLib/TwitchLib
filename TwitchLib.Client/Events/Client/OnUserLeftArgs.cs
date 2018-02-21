using System;

namespace TwitchLib.Client.Events.Client
{
    /// <inheritdoc />
    /// <summary>Args representing viewer left event.</summary>
    public class OnUserLeftArgs : EventArgs
    {
        /// <summary>Property representing username of user that left.</summary>
        public string Username;
        /// <summary>Property representing channel bot is connected to.</summary>
        public string Channel;
    }
}
