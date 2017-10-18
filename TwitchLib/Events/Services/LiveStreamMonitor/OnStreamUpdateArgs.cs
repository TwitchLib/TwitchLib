namespace TwitchLib.Events.Services.LiveStreamMonitor
{
    #region using directives
    using System;

    using Enums;
    #endregion
    /// <summary>Class representing event args for OnChannelOnline event.</summary>
    public class OnStreamUpdateArgs : EventArgs
    {
        /// <summary>Event property representing channel Id that has gone online.</summary>
        public string ChannelId;
        /// <summary>Event property representing channel that has gone online.</summary>
        public string Channel;
        /// <summary>Event property representing live stream information.</summary>
        public Models.API.v5.Streams.Stream Stream;
        /// <summary>Event property representing seconds between queries to Twitch Api.</summary>
        public int CheckIntervalSeconds;
    }
}
