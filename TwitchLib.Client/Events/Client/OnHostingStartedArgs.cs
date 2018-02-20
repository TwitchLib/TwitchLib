using System;

namespace TwitchLib.Client.Events.Client
{
    /// <inheritdoc />
    /// <summary>Args representing hosting started event.</summary>
    public class OnHostingStartedArgs : EventArgs
    {
        /// <summary>Property representing channel that started hosting.</summary>
        public string HostingChannel;
        /// <summary>Property representing targeted channel, channel being hosted.</summary>
        public string TargetChannel;
        /// <summary>Property representing number of viewers in channel hosting target channel.</summary>
        public int Viewers;
    }
}
