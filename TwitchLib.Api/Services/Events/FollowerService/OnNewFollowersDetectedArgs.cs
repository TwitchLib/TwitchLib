using System;
using System.Collections.Generic;
using TwitchLib.Api.Interfaces;

namespace TwitchLib.Api.Services.Events.FollowerService
{
    /// <inheritdoc />
    /// <summary>Class representing event args for OnNewFollowersDetected event.</summary>
    public class OnNewFollowersDetectedArgs : EventArgs
    {
        /// <summary>Event property representing whether channeldata is a channel name or channel id.</summary>
        public Enums.ChannelIdentifierType ChannelIdentifier;
        /// <summary>Event property representing channel the service is currently monitoring.</summary>
        public string ChannelData;
        /// <summary>Event property representing number of recent followers a query to Twitch Api should return.</summary>
        public int QueryCount;
        /// <summary>Event property representing seconds between queries to Twitch Api.</summary>
        public int CheckIntervalSeconds;
        /// <summary>Event property representing all new followers detected.</summary>
        public List<IFollow> NewFollowers;
    }
}
