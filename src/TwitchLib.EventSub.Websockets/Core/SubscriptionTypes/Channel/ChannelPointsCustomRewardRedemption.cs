using System;
using TwitchLib.EventSub.Websockets.Core.Models.ChannelPoints;

namespace TwitchLib.EventSub.Websockets.Core.SubscriptionTypes.Channel;

/// <summary>
/// Channel Points Custom Reward Redemption subscription type model
/// <para>!! The same for all channel points redemption subscription types !!</para>
/// <para>Description:</para>
/// <para>A viewer has redeemed a custom channel points reward on the specified channel.</para>
/// <para>A redemption of a channel points custom reward has been updated for the specified channel.</para>
/// </summary>
public class ChannelPointsCustomRewardRedemption
{
    /// <summary>
    /// The redemption identifier.
    /// </summary>
    public string Id { get; set; } = string.Empty;
    /// <summary>
    /// The requested broadcaster ID.
    /// </summary>
    public string BroadcasterUserId { get; set; } = string.Empty;
    /// <summary>
    /// The requested broadcaster display name.
    /// </summary>
    public string BroadcasterUserName { get; set; } = string.Empty;
    /// <summary>
    /// The requested broadcaster login.
    /// </summary>
    public string BroadcasterUserLogin { get; set; } = string.Empty;
    /// <summary>
    /// User ID of the user that redeemed the reward.
    /// </summary>
    public string UserId { get; set; } = string.Empty;
    /// <summary>
    /// Display name of the user that redeemed the reward.
    /// </summary>
    public string UserName { get; set; } = string.Empty;
    /// <summary>
    /// Login of the user that redeemed the reward.
    /// </summary>
    public string UserLogin { get; set; } = string.Empty;
    /// <summary>
    /// The user input provided. Empty string if not provided.
    /// </summary>
    public string UserInput { get; set; } = string.Empty;
    /// <summary>
    /// Status of the redemption. Possible values are unknown, unfulfilled, fulfilled, and canceled.
    /// </summary>
    public string Status { get; set; } = string.Empty;
    /// <summary>
    /// Basic information about the reward that was redeemed, at the time it was redeemed.
    /// </summary>
    public RedemptionReward Reward { get; set; } = new();
    /// <summary>
    /// RFC3339 timestamp of when the reward was redeemed.
    /// </summary>
    public DateTime RedeemedAt { get; set; } = DateTime.MinValue;
}