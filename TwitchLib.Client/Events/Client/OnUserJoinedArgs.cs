using System;

namespace TwitchLib.Events.Client
{
    /// <inheritdoc />
    /// <summary>Args representing viewer joined event.</summary>
    public class OnUserJoinedArgs : EventArgs
    {
        /// <summary>Property representing username of joined viewer.</summary>
        public string Username;
        /// <summary>Property representing channel bot is connected to.</summary>
        public string Channel;
    }
}
