using System.Collections.Generic;

namespace TwitchLib.Models.API
{
    /// <summary>Class representing response from Twitch API for channel Subscribers.</summary>
    public class ChannelSubscribersResponse
    {
        /// <summary>Property representing list of Subscriber objects.</summary>
        public List<Subscription> Subscribers { get; protected set; } = new List<Subscription>();
        /// <summary>Property representing total subscriber count.</summary>
        public int TotalSubscriberCount { get { return Subscribers.Count; } }
        /// <summary>Property representing cursor for pagination.</summary>
    }
}
