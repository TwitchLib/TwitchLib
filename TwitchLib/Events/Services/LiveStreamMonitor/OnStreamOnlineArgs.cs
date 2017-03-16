using System;
using TwitchLib.Enums;

namespace TwitchLib.Events.Services.LiveStreamMonitor
{
    /// <summary>Class representing event args for OnChannelOnline event.</summary>
    public class OnStreamOnlineArgs : EventArgs
    {
        /// <summary>Event property representing channel that has gone online.</summary>
        public string Channel;
        /// <summary>Event property representing how channels IDs are represented.</summary>
        public StreamIdentifierType IdentifierType;
        /// <summary>Event property representing seconds between queries to Twitch Api.</summary>
        public int CheckIntervalSeconds;
    }
}
