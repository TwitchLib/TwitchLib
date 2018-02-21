using System;
using TwitchLib.Api.Models.v5.Streams;

namespace TwitchLib.Api.Services.Events.LiveStreamMonitor
{
    /// <inheritdoc />
    /// <summary>Class representing event args for OnChannelOnline event.</summary>
    public class OnStreamOnlineArgs : EventArgs
    {
        /// <summary>Event property representing channel Id that has gone online.</summary>
        public string ChannelId;
        /// <summary>Event property representing channel that has gone online.</summary>
        public string Channel;
        /// <summary>Event property representing live stream information.</summary>
        public Stream Stream;
        /// <summary>Event property representing seconds between queries to Twitch Api.</summary>
        public int CheckIntervalSeconds;
    }
}
