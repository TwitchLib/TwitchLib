namespace TwitchLib.Events.Services.FollowerService
{
    #region using directives
    using System;
    #endregion
    /// <summary>Class representing event args for OnServiceStopped event.</summary>
    public class OnServiceStoppedArgs : EventArgs
    {
        /// <summary>Event property representing whether channel data is a channel name or a channel id.</summary>
        public Enums.ChannelIdentifierType ChannelIdentifier;
        /// <summary>Event property representing channel the service is currently monitoring.</summary>
        public string ChannelData;
        /// <summary>Event property representing number of recent followers a query to Twitch Api should return.</summary>
        public int QueryCount;
        /// <summary>Event property representing seconds between queries to Twitch Api.</summary>
        public int CheckIntervalSeconds;
    }
}
