using System;

namespace TwitchLib.PubSub.Events
{
    /// <inheritdoc />
    /// <summary>
    /// Class representing arguments of custom reward deleted event.
    /// </summary>
    public class OnCustomRewardDeletedArgs
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
        /// Property representing the id of the deleted reward
        /// </summary>
        public Guid RewardId;
        /// <summary>
        /// Property representing title of the deleted reward
        /// </summary>
        public string RewardTitle;
        /// <summary>
        /// Property representing prompt of the deleted reward
        /// </summary>
        public string RewardPrompt;
    }
}
