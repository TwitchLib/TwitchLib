using System;

namespace TwitchLib.Events.Client
{
    /// <summary>Args representing moderator joined event.</summary>
    public class OnModeratorJoinedArgs : EventArgs
    {
        /// <summary>Property representing username of joined moderator.</summary>
        public string Username;
        /// <summary>Property representing channel bot is connected to.</summary>
        public string Channel;
    }
}
