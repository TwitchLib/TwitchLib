using System;

namespace TwitchLib.Events.Client
{
    /// <summary>Args representing a successful chat color change request.</summary>
    public class OnChatColorChangedArgs : EventArgs
    {
        /// <summary>Property reprenting the channel the event was received in.</summary>
        public string Channel;
    }
}
