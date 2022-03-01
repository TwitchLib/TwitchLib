using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Schedule.UpdateChannelStreamSegment
{
    public class UpdateChannelStreamSegmentResponse
    {
        [JsonPropertyName("data")]
        public ChannelStreamSchedule Schedule { get; protected set; }
    }
}