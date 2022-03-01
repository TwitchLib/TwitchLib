using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.PubSub.Models.Responses.Messages.Redemption
{
    public class Redemption
    {
        [JsonPropertyName("id")]
        public string Id { get; protected set; }
        [JsonPropertyName("user")]
        public User User { get; protected set; }
        [JsonPropertyName("channel_id")]
        public string ChannelId { get; protected set; }
        [JsonPropertyName("redeemed_at")]
        public DateTime RedeemedAt { get; protected set; }
        [JsonPropertyName("reward")]
        public Reward Reward { get; protected set; }
        [JsonPropertyName("user_input")]
        public string UserInput { get; protected set; }
        [JsonPropertyName("status")]
        public string Status { get; protected set; }
    }
}
