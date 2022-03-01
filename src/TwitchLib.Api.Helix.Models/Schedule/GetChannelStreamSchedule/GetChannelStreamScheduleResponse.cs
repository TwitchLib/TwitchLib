using System.Text.Json.Serialization;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Schedule.GetChannelStreamSchedule
{
    public class GetChannelStreamScheduleResponse
    {
        [JsonPropertyName("data")]
        public ChannelStreamSchedule Schedule { get; protected set; }
        [JsonPropertyName("pagination")]
        public Pagination Pagination { get; protected set; }
    }
}