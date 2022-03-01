using System.Text.Json.Serialization;
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.Helix.Models.ChannelPoints.UpdateCustomRewardRedemptionStatus
{
    public class UpdateCustomRewardRedemptionStatusRequest
    {
        //[JsonConverter(typeof(StringEnumConverter))]
        [JsonPropertyName("status")]
        public CustomRewardRedemptionStatus Status { get; set; }
    }
}
