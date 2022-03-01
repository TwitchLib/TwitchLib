using System;
using TwitchLib.EventSub.Webhooks.Core.Models.Polls;

namespace TwitchLib.EventSub.Webhooks.Core.SubscriptionTypes.Channel
{
    /// <summary>
    /// Channel Poll Begin subscription type model
    /// <para>Description:</para>
    /// <para>A poll started on a specified channel.</para>
    /// </summary>
    public class ChannelPollBegin : ChannelPollBase
    {
        /// <summary>
        /// The time the poll will end.
        /// </summary>
        public DateTime EndsAt { get; set; } = DateTime.MinValue;
    }
}