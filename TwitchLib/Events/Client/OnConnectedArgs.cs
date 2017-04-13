using System;

namespace TwitchLib.Events.Client
{
    /// <summary>Args representing on connected event.</summary>
    public class OnConnectedArgs : EventArgs
    {
        /// <summary>Property representing bot username.</summary>
        public string Username;
        /// <summary>Property representing connected channel.</summary>
        public string AutoJoinChannel;
    }
}
