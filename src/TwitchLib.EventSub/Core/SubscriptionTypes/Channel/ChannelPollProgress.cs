using System;
using TwitchLib.EventSub.Core.Models.Polls;

namespace TwitchLib.EventSub.Core.SubscriptionTypes.Channel
{
    /// <summary>
    /// Channel Poll Progress subscription type model
    /// <para>Description:</para>
    /// <para>Users respond to a poll on a specified channel.</para>
    /// </summary>
    public class ChannelPollProgress : ChannelPollBase
    {
        /// <summary>
        /// The time the poll will end.
        /// </summary>
        public DateTime EndsAt { get; set; } = DateTime.MinValue;
    }
}