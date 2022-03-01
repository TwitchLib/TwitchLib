using System;
using TwitchLib.Api.Helix.Models.Streams;
using TwitchLib.Api.Helix.Models.Streams.GetStreams;

namespace TwitchLib.Api.Services.Events.LiveStreamMonitor
{
    /// <inheritdoc />
    /// <summary>
    /// Class representing EventArgs for OnStreamOffline event.
    /// </summary>
    public class OnStreamOfflineArgs : EventArgs
    {
        /// <summary>
        /// The channel that has gone online.
        /// </summary>
        public string Channel;
        /// <summary>
        /// The channel's live stream information.
        /// </summary>
        public Stream Stream;
    }
}
