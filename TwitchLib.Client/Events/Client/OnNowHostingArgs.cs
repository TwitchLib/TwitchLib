using System;

namespace TwitchLib.Client.Events.Client
{
    /// <inheritdoc />
    /// <summary>Args representing the detected hosted channel.</summary>
    public class OnNowHostingArgs : EventArgs
    {
        /// <summary>Property the channel that received the event.</summary>
        public string Channel;
        /// <summary>Property representing channel that is being hosted.</summary>
        public string HostedChannel;
    }
}
