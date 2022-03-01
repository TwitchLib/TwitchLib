using System;

namespace TwitchLib.PubSub.Events
{
    /// <inheritdoc />
    /// <summary>
    /// Object representing the arguments for bits received event
    /// </summary>
    public class OnBitsReceivedArgs : EventArgs
    {
        /// <summary>
        /// Property of for username.
        /// </summary>
        public string Username;
        /// <summary>
        /// Property for channel name.
        /// </summary>
        public string ChannelName;
        /// <summary>
        /// Property for user id.
        /// </summary>
        public string UserId;
        /// <summary>
        /// Property for channel id.
        /// </summary>
        public string ChannelId;
        /// <summary>
        /// Property for time.
        /// </summary>
        public string Time;
        /// <summary>
        /// Property for chat message
        /// </summary>
        public string ChatMessage;
        /// <summary>
        /// Property for bits used.
        /// </summary>
        public int BitsUsed;
        /// <summary>
        /// Property for total bits used.
        /// </summary>
        public int TotalBitsUsed;
        /// <summary>
        /// Property for context
        /// </summary>
        public string Context;
    }
}
