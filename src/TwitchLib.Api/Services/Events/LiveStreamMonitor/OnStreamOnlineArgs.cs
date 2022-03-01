using System;
using TwitchLib.Api.Helix.Models.Streams.GetStreams;

namespace TwitchLib.Api.Services.Events.LiveStreamMonitor
{
    /// <inheritdoc />
    /// <summary>
    /// Class representing EventArgs for OnStreamOnline event.
    /// </summary>
    public class OnStreamOnlineArgs : EventArgs
    {
        /// <summary>
        /// Event property representing channel that has gone online.
        /// </summary>
        public string Channel;
        /// <summary>
        /// Event property representing live stream information.
        /// </summary>
        public Stream Stream;
    }
}
