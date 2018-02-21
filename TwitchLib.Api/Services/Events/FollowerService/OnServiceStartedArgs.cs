using System;

namespace TwitchLib.Api.Services.Events.FollowerService
{
    /// <inheritdoc />
    /// <summary>Class representing event args for OnServiceStarted event.</summary>
    public class OnServiceStartedArgs : EventArgs
    {
        /// <summary>Event property representing whether channeldata is a channel name or channel Id.</summary>
        public Enums.ChannelIdentifierType ChannelIdentifier;
        /// <summary>Event property representing channel the service is currently monitoring.</summary>
        public string ChannelData;
        /// <summary>Event property representing number of recent followers a query to Twitch Api should return.</summary>
        public int QueryCount;
        /// <summary>Event property representing seconds between queries to Twitch Api.</summary>
        public int CheckIntervalSeconds;
    }
}
