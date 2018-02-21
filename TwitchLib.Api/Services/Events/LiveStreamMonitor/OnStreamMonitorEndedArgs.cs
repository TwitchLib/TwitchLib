using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace TwitchLib.Api.Services.Events.LiveStreamMonitor
{
    /// <inheritdoc />
    /// <summary>Class representing event args for OnChannelMonitorEnded event.</summary>
    public class OnStreamMonitorEndedArgs : EventArgs
    {
        /// <summary>Event property representing channels the service is currently monitoring.</summary>
        public List<string> ChannelIds;
        /// <summary>Event property representing channels the service is currently monitoring.</summary>
        public ConcurrentDictionary<string, string> Channels;
        /// <summary>Event property representing seconds between queries to Twitch Api.</summary>
        public int CheckIntervalSeconds;
    }
}
