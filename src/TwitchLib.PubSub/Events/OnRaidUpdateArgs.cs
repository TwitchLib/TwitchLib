using System;

namespace TwitchLib.PubSub.Events
{
    /// <inheritdoc />
    /// <summary>
    /// Class representing arguments of on raid update event.
    /// </summary>
    public class OnRaidUpdateArgs : EventArgs
    {
        /// <summary>
        /// The channel ID the event came from
        /// </summary>
        public string ChannelId;
        /// <summary>
        /// Property representing the id of the raid event
        /// </summary>
        public Guid Id;
        /// <summary>
        /// Property representing the target channel id
        /// </summary>
        public string TargetChannelId;
        /// <summary>
        /// Property representing the date when the raid got prepared
        /// </summary>
        public DateTime AnnounceTime;
        /// <summary>
        /// Property representing the date the raid starts
        /// </summary>
        public DateTime RaidTime;
        /// <summary>
        /// Property representing the countdown for the raid
        /// </summary>
        public int RemainingDurationSeconds;
        /// <summary>
        /// Property representing the count of people in the raid
        /// </summary>
        public int ViewerCount;
    }
}
