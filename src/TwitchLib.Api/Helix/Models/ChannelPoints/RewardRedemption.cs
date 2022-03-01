using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.Helix.Models.ChannelPoints
{
    public class RewardRedemption
    {
        [JsonPropertyName("broadcaster_id")]
        public string BroadcasterId { get; protected set; }
        [JsonPropertyName("broadcaster_login")]
        public string BroadcasterLogin { get; protected set; }
        [JsonPropertyName("id")]
        public string Id { get; protected set; }
        [JsonPropertyName("user_id")]
        public string UserId { get; protected set; }
        [JsonPropertyName("user_login")]
        public string UserLogin { get; protected set; }
        [JsonPropertyName("user_name")]
        public string UserName { get; protected set; }
        [JsonPropertyName("user_input")]
        public string UserInput { get; protected set; }
        [JsonPropertyName("status")]
        public CustomRewardRedemptionStatus Status { get; protected set; }
        [JsonPropertyName("redeemed_at")]
        public DateTime RedeemedAt { get; protected set; }
        [JsonPropertyName("reward")]
        public Reward Reward { get; protected set; }
    }
}
