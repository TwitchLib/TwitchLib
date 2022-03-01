using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.PubSub.Models.Responses.Messages.Redemption
{
    public class RewardRedeemed : ChannelPointsData
    {
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; protected set; }
        [JsonPropertyName("redemption")]
        public Redemption Redemption { get; protected set; }
    }
}
