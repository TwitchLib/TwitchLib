namespace TwitchLib.Events.Services.FollowerService
{
    #region using directives
    using System;
    using System.Collections.Generic;
    #endregion
    /// <summary>Class representing event args for OnNewFollowersDetected event.</summary>
    public class OnNewFollowersDetectedFullArgs : EventArgs
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
        public List<Interfaces.IFollow> NewFollowers;
    }
}
