using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Teams
{
    public class GetTeamsResponse
    {
        [JsonPropertyName("data")]
        public Team[] Teams { get; protected set; }
    }
}