namespace TwitchLib.EventSub.Websockets.Core.SubscriptionTypes.Channel;

/// <summary>
/// Channel Subscription Gift subscription type model
/// <para>Description:</para>
/// <para>A notification when a viewer gives a gift subscription to one or more users in the specified channel.</para>
/// </summary>
public class ChannelSubscriptionGift
{
    /// <summary>
    /// The user ID of the user who sent the subscription gift. Set to null if it was an anonymous subscription gift.
    /// </summary>
    public string? UserId { get; set; } = string.Empty;
    /// <summary>
    /// The user display name of the user who sent the gift. Set to null if it was an anonymous subscription gift.
    /// </summary>
    public string? UserName { get; set; } = string.Empty;
    /// <summary>
    /// The user login of the user who sent the gift. Set to null if it was an anonymous subscription gift.
    /// </summary>
    public string? UserLogin { get; set; } = string.Empty;
    /// <summary>
    /// The broadcaster user ID.
    /// </summary>
    public string BroadcasterUserId { get; set; } = string.Empty;
    /// <summary>
    /// The broadcaster display name.
    /// </summary>
    public string BroadcasterUserName { get; set; } = string.Empty;
    /// <summary>
    /// The broadcaster login.
    /// </summary>
    public string BroadcasterUserLogin { get; set; } = string.Empty;
    /// <summary>
    /// The number of subscriptions in the subscription gift.
    /// </summary>
    public int Total { get; set; }
    /// <summary>
    /// The tier of subscriptions in the subscription gift. Valid values are 1000, 2000, and 3000.
    /// </summary>
    public string Tier { get; set; } = string.Empty;
    /// <summary>
    /// The number of subscriptions gifted by this user in the channel. This value is null for anonymous gifts or if the gifter has opted out of sharing this information.
    /// </summary>
    public int? CumulativeTotal { get; set; }
    /// <summary>
    /// Whether the subscription gift was anonymous.
    /// </summary>
    public bool IsAnonymous { get; set; }
}