using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.ChannelPoints.UpdateCustomRewardRedemptionStatus
{
    public class UpdateCustomRewardRedemptionStatusResponse
    {
        [JsonPropertyName("data")]
        public RewardRedemption[] Data { get; protected set; }
    }
}
