using System;

namespace TwitchLib.Events.Client
{
    /// <summary>Args representing client disconnect event.</summary>
    public class OnDisconnectedArgs : EventArgs
    {
        /// <summary>Username of the bot that was disconnected.</summary>
        public string Username;
    }
}
