using System;

namespace TwitchLib.PubSub.Events
{
    /// <inheritdoc />
    /// <summary>
    /// Object representing the arguments for a raid go event
    /// </summary>
    public class OnRaidGoArgs : EventArgs
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
        /// Property representing the target channel login
        /// </summary>
        public string TargetLogin;
        /// <summary>
        /// Property representing  the target display name
        /// </summary>
        public string TargetDisplayName;
        /// <summary>
        /// Property representing the target profile image url
        /// </summary>
        public string TargetProfileImage;
        /// <summary>
        /// Property representing the count of people in the raid
        /// </summary>
        public int ViewerCount;
    }
}
