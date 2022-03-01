using System;

namespace TwitchLib.EventSub.Websockets.Core.Models.ChannelGoals;

/// <summary>
/// Channel Goal base class
/// </summary>
public abstract class ChannelGoalBase
{
    /// <summary>
    /// An ID that identifies this event.
    /// </summary>
    public string Id { get; set; } = string.Empty;
    /// <summary>
    /// An ID that uniquely identifies the broadcaster.
    /// </summary>
    public string BroadcasterUserId { get; set; } = string.Empty;
    /// <summary>
    /// The broadcaster’s display name.
    /// </summary>
    public string BroadcasterUserName { get; set; } = string.Empty;
    /// <summary>
    /// The broadcaster’s user handle.
    /// </summary>
    public string BroadcasterUserLogin { get; set; } = string.Empty;
    /// <summary>
    /// The type of goal. Possible values are: followers, subscriptions
    /// </summary>
    public string Type { get; set; } = string.Empty;
    /// <summary>
    /// A description of the goal, if specified. The description may contain a maximum of 40 characters.
    /// </summary>
    public string Description { get; set; } = string.Empty;
    /// <summary>
    /// The current value.
    /// <para>If the goal is to increase followers, this field is set to the current number of followers.</para>
    /// <para>This number increases with new followers and decreases if users unfollow the channel.</para>
    /// <para>For subscriptions, CurrentAmount is increased and decreased by the points value associated with the subscription tier. </para>
    /// <para>For example, if a tier-two subscription is worth 2 points, CurrentAmount is increased or decreased by 2, not 1.</para>
    /// </summary>
    public int CurrentAmount { get; set; }
    /// <summary>
    /// The goal’s target value.
    /// <para>For example, if the broadcaster has 200 followers before creating the goal,</para>
    /// <para>and their goal is to double that number, this field is set to 400.</para>
    /// </summary>
    public int TargetAmount { get; set; }
    /// <summary>
    /// The UTC timestamp in RFC 3339 format, which indicates when the broadcaster created the goal.
    /// </summary>
    public DateTime StartedAt { get; set; } = DateTime.MinValue;
}