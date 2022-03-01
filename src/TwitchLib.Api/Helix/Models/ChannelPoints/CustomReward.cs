using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.ChannelPoints
{
    public class CustomReward
    {
        [JsonPropertyName("broadcaster_id")]
        public string BroadcasterId { get; protected set; }
        [JsonPropertyName("broadcaster_login")]
        public string BroadcasterLogin { get; protected set; }
        [JsonPropertyName("broadcaster_name")]
        public string BroadcasterName { get; protected set; }
        [JsonPropertyName("id")]
        public string Id { get; protected set; }
        [JsonPropertyName("title")]
        public string Title { get; protected set; }
        [JsonPropertyName("prompt")]
        public string Prompt { get; protected set; }
        [JsonPropertyName("cost")]
        public int Cost { get; protected set; }
        [JsonPropertyName("image")]
        public Image Image { get; protected set; }
        [JsonPropertyName("default_image")]
        public DefaultImage DefaultImage { get; protected set; }
        [JsonPropertyName("background_color")]
        public string BackgroundColor { get; protected set; }
        [JsonPropertyName("is_enabled")]
        public bool IsEnabled { get; protected set; }
        [JsonPropertyName("is_user_input_required")]
        public bool IsUserInputRequired { get; protected set; }
        [JsonPropertyName("max_per_stream_setting")]
        public MaxPerStreamSetting MaxPerStreamSetting { get; protected set; }
        [JsonPropertyName("max_per_user_per_stream_setting")]
        public MaxPerStreamSetting MaxPerUserPerStreamSetting { get; protected set; }
        [JsonPropertyName("global_cooldown_setting")]
        public GlobalCooldownSetting GlobalCooldownSetting { get; protected set; }
        [JsonPropertyName("is_paused")]
        public bool IsPaused { get; protected set; }
        [JsonPropertyName("is_in_stock")]
        public bool IsInStock { get; protected set; }
        [JsonPropertyName("should_redemptions_skip_request_queue")]
        public bool ShouldRedemptionsSkipQueue { get; protected set; }
        [JsonPropertyName("redemptions_redeemed_current_stream")]
        public int? RedemptionsRedeemedCurrentStream { get; protected set; }
        [JsonPropertyName("cooldown_expires_at")]
        public string CooldownExpiresAt { get; protected set; }
    }
}
