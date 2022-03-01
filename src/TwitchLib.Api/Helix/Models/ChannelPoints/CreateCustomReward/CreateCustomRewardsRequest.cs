using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.ChannelPoints.CreateCustomReward
{
    public class CreateCustomRewardsRequest
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("prompt")]
        public string Prompt { get; set; }
        [JsonPropertyName("cost")]
        public int Cost { get; set; }
        [JsonPropertyName("is_enabled")]
        public bool IsEnabled { get; set; }
        [JsonPropertyName("background_color")]
        public string BackgroundColor { get; set; }
        [JsonPropertyName("is_user_input_required")]
        public bool IsUserInputRequired { get; set; }
        [JsonPropertyName("is_max_per_stream-Enabled")]
        public bool IsMaxPerStreamEnabled { get; set; }
        [JsonPropertyName("max_per_stream")]
        public int? MaxPerStream { get; set; }
        [JsonPropertyName("is_max_per_user_per_stream_enabled")]
        public bool IsMaxPerUserPerStreamEnabled { get; set; }
        [JsonPropertyName("max_per_user_per_stream")]
        public int? MaxPerUserPerStream { get; set; }
        [JsonPropertyName("is_global_cooldown_enabled")]
        public bool IsGlobalCooldownEnabled { get; set; }
        [JsonPropertyName("global_cooldown_seconds")]
        public int? GlobalCooldownSeconds { get; set; }
        [JsonPropertyName("should_redemptions_skip_request_queue")]
        public bool ShouldRedemptionsSkipRequestQueue { get; set; }
    }
}
