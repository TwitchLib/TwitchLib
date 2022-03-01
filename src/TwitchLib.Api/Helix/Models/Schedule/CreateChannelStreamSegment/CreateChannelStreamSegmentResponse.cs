using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Schedule.CreateChannelStreamSegment
{
    public class CreateChannelStreamSegmentResponse
    {
        [JsonPropertyName("data")]
        public ChannelStreamSchedule Schedule { get; protected set; }
    }
}