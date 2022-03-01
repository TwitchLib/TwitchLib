using System;

namespace TwitchLib.PubSub.Events
{
    /// <inheritdoc />
    /// <summary>
    /// Class representing arguments of custom reward created event.
    /// </summary>
    public class OnCustomRewardCreatedArgs : EventArgs
    {
        /// <summary>
        /// Property representing server time stamp
        /// </summary>
        public DateTime TimeStamp;
        /// <summary>
        /// The channel ID the event came from
        /// </summary>
        public string ChannelId;
        /// <summary>
        /// Property representing the id of the created reward
        /// </summary>
        public Guid RewardId;
        /// <summary>
        /// Property representing title of the created reward
        /// </summary>
        public string RewardTitle;
        /// <summary>
        /// Property representing prompt of the created reward
        /// </summary>
        public string RewardPrompt;
        /// <summary>
        /// Property representing cost of the created reward
        /// </summary>
        public int RewardCost;
    }
}
