namespace TwitchLib.Events.Services.LiveStreamMonitor
{
    #region using directives
    using System;
    using System.Collections.Generic;

    using Enums;
    #endregion
    /// <summary>Class representing event args for OnChannelMonitorEnded event.</summary>
    public class OnStreamMonitorEndedArgs : EventArgs
    {
        /// <summary>Event property representing channels the service is currently monitoring.</summary>
        public List<string> ChannelIds;
        /// <summary>Event property representing channels the service is currently monitoring.</summary>
        public Dictionary<string, string> Channels;
        /// <summary>Event property representing seconds between queries to Twitch Api.</summary>
        public int CheckIntervalSeconds;
    }
}
