namespace TwitchLib.EventSub.Websockets.Core.SubscriptionTypes.Channel;

/// <summary>
/// Channel Update subscription type model
/// <para>Description:</para>
/// <para>A broadcaster updates their channel properties e.g., category, title, mature flag, broadcast, or language.</para>
/// </summary>
public class ChannelUpdate
{
    /// <summary>
    /// The broadcaster’s user ID.
    /// </summary>
    public string BroadcasterUserId { get; set; } = string.Empty;
    /// <summary>
    /// The broadcaster’s user display name.
    /// </summary>
    public string BroadcasterUserName { get; set; } = string.Empty;
    /// <summary>
    /// The broadcaster’s user login.
    /// </summary>
    public string BroadcasterUserLogin { get; set; } = string.Empty;
    /// <summary>
    /// The channel’s stream title.
    /// </summary>
    public string Title { get; set; } = string.Empty;
    /// <summary>
    /// The channel’s broadcast language.
    /// </summary>
    public string Language { get; set; } = string.Empty;
    /// <summary>
    /// The channel’s category ID.
    /// </summary>
    public string CategoryId { get; set; } = string.Empty;
    /// <summary>
    /// The channel’s category name.
    /// </summary>
    public string CategoryName { get; set; } = string.Empty;
    /// <summary>
    /// A boolean identifying whether the channel is flagged as mature.
    /// </summary>
    public bool IsMature { get; set; }
}