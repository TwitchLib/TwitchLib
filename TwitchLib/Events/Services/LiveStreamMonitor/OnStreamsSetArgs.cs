using System;
using System.Collections.Generic;
using TwitchLib.Enums;
namespace TwitchLib.Events.Services.LiveStreamMonitor
{
    /// <summary>Class representing event args for StreamsSet event.</summary>
    public class OnStreamsSetArgs : EventArgs
    {
        /// <summary>Event property representing channels the service is currently monitoring.</summary>
        public List<String> Channels;
        /// <summary>Event property representing how channels IDs are represented.</summary>
        public StreamIdentifierType IdentifierType;
        /// <summary>Event property representing seconds between queries to Twitch Api.</summary>
        public int CheckIntervalSeconds;
    }
}
