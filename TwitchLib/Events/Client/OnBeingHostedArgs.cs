using System;

namespace TwitchLib.Events.Client
{
    /// <inheritdoc />
    /// <summary>Args representing an event where another channel has started hosting the broadcaster's channel.</summary>
    public class OnBeingHostedArgs : EventArgs
    {
        /// <summary>Property representing bot username that received this event.</summary>
        public string BotUsername;
        /// <summary>Property representing the channel that has started hosting the broadcaster's channel.</summary>
        public string HostedByChannel;
        /// <summary>Property representing the number of viewers in the host. If not detected, will be -1.</summary>
        public int Viewers;
        /// <summary>Property representing the channel received state from.</summary>
        public string Channel;
        /// <summary>Property representing whether or not host was auto or not.</summary>
        public bool IsAutoHosted;
    }
}
