using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.PubSub.Events
{
    public class OnBitsReceivedV2Args
    {
        /// <summary>
        /// Property of username.
        /// </summary>
        public string UserName { get; internal set; }
        /// <summary>
        /// Property of channel name.
        /// </summary>
        public string ChannelName { get; internal set; }
        /// <summary>
        /// Property of user id.
        /// </summary>
        public string UserId { get; internal set; }
        /// <summary>
        /// Property of channel id.
        /// </summary>
        public string ChannelId { get; internal set; }
        /// <summary>
        /// Property of time.
        /// </summary>
        public DateTime Time { get; internal set; }
        /// <summary>
        /// Property of chat message.
        /// </summary>
        public string ChatMessage { get; internal set; }
        /// <summary>
        /// Property of bits used.
        /// </summary>
        public int BitsUsed { get; internal set; }
        /// <summary>
        /// Property of total bits used.
        /// </summary>
        public int TotalBitsUsed { get; internal set; }
        /// <summary>
        /// Property of whether or not the bits were sent anonymously
        /// </summary>
        public bool IsAnonymous { get; internal set; }
        /// <summary>
        /// Property representing type
        /// </summary>
        public string Context { get; internal set; }
    }
}
