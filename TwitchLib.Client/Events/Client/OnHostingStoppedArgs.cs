using System;

namespace TwitchLib.Client.Events.Client
{
    /// <inheritdoc />
    /// <summary>Args representing hosting stopped event.</summary>
    public class OnHostingStoppedArgs : EventArgs
    {
        /// <summary>Property representing hosting channel.</summary>
        public string HostingChannel;
        /// <summary>Property representing number of viewers that were in hosting channel.</summary>
        public int Viewers;
    }
}
