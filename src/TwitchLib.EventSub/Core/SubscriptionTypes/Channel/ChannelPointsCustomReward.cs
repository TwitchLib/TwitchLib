using System;
using System.Collections.Generic;
using TwitchLib.EventSub.Core.Models.ChannelPoints;

namespace TwitchLib.EventSub.Core.SubscriptionTypes.Channel
{
    /// <summary>
    /// Channel Points Custom Reward subscription type model
    /// <para>!! The same for all channel points subscription types !!</para>
    /// <para>Description:</para>
    /// <para>A custom channel points reward has been created/updated/removed for the specified channel.</para>
    /// </summary>
    public class ChannelPointsCustomReward
    {
        /// <summary>
        /// The reward identifier.
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
        /// Whether the reward currently enabled. If false, the reward won’t show up to viewers.
        /// </summary>
        public bool IsEnabled { get; set; }
        /// <summary>
        /// Whether the reward currently paused. If true, viewers can’t redeem.
        /// </summary>
        public bool IsPaused { get; set; }
        /// <summary>
        /// Whether the reward currently in stock. If false, viewers can’t redeem.
        /// </summary>
        public bool IsInStock { get; set; }
        /// <summary>
        /// The reward title.
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// The reward cost.
        /// </summary>
        public int Cost { get; set; }
        /// <summary>
        /// The reward description.
        /// </summary>
        public string Prompt { get; set; } = string.Empty;
        /// <summary>
        /// Does the viewer need to enter information when redeeming the reward.
        /// </summary>
        public bool IsUserInputRequired { get; set; }
        /// <summary>
        /// Should redemptions be set to fulfilled status immediately when redeemed and skip the request queue instead of the normal unfulfilled status.
        /// </summary>
        public bool ShouldRedemptionsSkipRequestQueue { get; set; }
        /// <summary>
        /// Timestamp of the cooldown expiration. null if the reward isn’t on cooldown.
        /// </summary>
        public DateTime? CooldownExpiresAt { get; set; }
        /// <summary>
        /// The number of redemptions redeemed during the current live stream.
        /// <para>Counts against the max_per_stream limit. null if the broadcasters stream isn’t live or max_per_stream isn’t enabled.</para>
        /// </summary>
        public int? RedemptionsRedeemedCurrentStream { get; set; }
        /// <summary>
        /// Whether a maximum per stream is enabled and what the maximum is.
        /// </summary>
        public MaxAmountSettings MaxPerStream { get; set; } = new();
        /// <summary>
        /// Whether a maximum per user per stream is enabled and what the maximum is.
        /// </summary>
        public MaxAmountSettings MaxPerUserPerStream { get; set; } = new();
        /// <summary>
        /// Whether a cooldown is enabled and what the cooldown is in seconds.
        /// </summary>
        public GlobalCooldownSettings GlobalCooldown { get; set; } = new();
        /// <summary>
        /// Custom background color for the reward. Format: Hex with # prefix. Example: #FA1ED2.
        /// </summary>
        public string BackgroundColor { get; set; } = string.Empty;
        /// <summary>
        /// Set of custom images of 1x, 2x and 4x sizes for the reward. Can be null if no images have been uploaded.
        /// </summary>
        public Dictionary<string, string>? Image { get; set; }
        /// <summary>
        /// Set of default images of 1x, 2x and 4x sizes for the reward.
        /// </summary>
        public Dictionary<string, string> DefaultImage { get; set; } = new();
    }
}