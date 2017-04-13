using System;

namespace TwitchLib.Events.Client
{
    /// <summary>Args representing on channel joined event.</summary>
    public class OnJoinedChannelArgs : EventArgs
    {
        /// <summary>Property representing bot username.</summary>
        public string Username;
        /// <summary>Property representing the channel that was joined.</summary>
        public string Channel;
    }
}
