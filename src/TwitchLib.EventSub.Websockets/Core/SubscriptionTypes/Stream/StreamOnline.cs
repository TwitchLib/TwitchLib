using System;

namespace TwitchLib.EventSub.Websockets.Core.SubscriptionTypes.Stream;

/// <summary>
/// Stream Online subscription type model
/// <para>Description:</para>
/// <para>The specified broadcaster starts a stream.</para>
/// </summary>
public class StreamOnline
{
    /// <summary>
    /// The id of the stream.
    /// </summary>
    public string Id { get; set; } = string.Empty;
    /// <summary>
    /// The broadcaster's user id.
    /// </summary>
    public string BroadcasterUserId { get; set; } = string.Empty;
    /// <summary>
    /// The broadcaster's user display name.
    /// </summary>
    public string BroadcasterUserName { get; set; } = string.Empty;
    /// <summary>
    /// The broadcaster's user login.
    /// </summary>
    public string BroadcasterUserLogin { get; set; } = string.Empty;
    /// <summary>
    /// The stream type. Valid values are: live, playlist, watch_party, premiere, rerun
    /// </summary>
    public string Type { get; set; } = string.Empty;
    /// <summary>
    /// The timestamp at which the stream went online at.
    /// </summary>
    public DateTime StartedAt { get; set; } = DateTime.MinValue;
}