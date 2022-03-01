using System;
using TwitchLib.EventSub.Websockets.Core.Models.ChannelGoals;

namespace TwitchLib.EventSub.Websockets.Core.SubscriptionTypes.Channel;

/// <summary>
/// Channel Goal End subscription type model
/// <para>Description:</para>
/// <para>A channel goal ends</para>
/// </summary>
public class ChannelGoalEnd : ChannelGoalBase
{
    /// <summary>
    /// The UTC timestamp in RFC 3339 format, which indicates when the broadcaster ended the goal.
    /// </summary>
    public DateTime EndedAt { get; set; } = DateTime.MinValue;
    /// <summary>
    /// A Boolean value that indicates whether the broadcaster achieved their goal. Is true if the goal was achieved; otherwise, false.
    /// </summary>
    public bool IsAchieved { get; set; }
}