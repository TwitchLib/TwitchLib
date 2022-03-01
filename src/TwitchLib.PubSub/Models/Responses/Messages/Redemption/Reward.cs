using System.Text.Json.Serialization;
using System;

namespace TwitchLib.PubSub.Models.Responses.Messages.Redemption
{
    public class Reward
    {
        [JsonPropertyName("id")]
        public string Id { get; protected set; }
        [JsonPropertyName("channel_id")]
        public string ChannelId { get; protected set; }
        [JsonPropertyName("title")]
        public string Title { get; protected set; }
        [JsonPropertyName("prompt")]
        public string Prompt { get; protected set; }
        [JsonPropertyName("cost")]
        public int Cost { get; protected set; }
        [JsonPropertyName("is_user_input_required")]
        public bool IsUserInputRequired { get; protected set; }
        [JsonPropertyName("is_sub_only")]
        public bool IsSubOnly { get; protected set; }
        [JsonPropertyName("image")]
        public RedemptionImage Image { get; protected set; }
        [JsonPropertyName("default_image")]
        public RedemptionImage DefaultImage { get; protected set; }
        [JsonPropertyName("background_color")]
        public string BackgroundColor { get; protected set; }
        [JsonPropertyName("is_enabled")]
        public bool IsEnabled { get; protected set; }
        [JsonPropertyName("is_paused")]
        public bool IsPaused { get; protected set; }
        [JsonPropertyName("is_in_stock")]
        public bool IsInStock { get; protected set; }
        [JsonPropertyName("max_per_stream")]
        public MaxPerStream MaxPerStream { get; protected set; }
        [JsonPropertyName("should_redemptions_skip_request_queue")]
        public bool ShouldRedemptionsSkipRequestQueue { get; protected set; }
        [JsonPropertyName("template_id")]
        public string TemplateId { get; protected set; }
        [JsonPropertyName("updated_for_indicator_at")]
        public DateTime UpdatedForIndicatorAt { get; protected set; }
        [JsonPropertyName("max_per_user_per_stream")]
        public MaxPerUserPerStream MaxPerUserPerStream { get; protected set; }
        [JsonPropertyName("global_cooldown")]
        public GlobalCooldown GlobalCooldown { get; protected set; }
        [JsonPropertyName("cooldown_expires_at")]
        public DateTime? CooldownExpiresAt { get; protected set; }
    }
}
