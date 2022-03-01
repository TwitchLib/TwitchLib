using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Teams
{
    public class GetChannelTeamsResponse
    {
        [JsonPropertyName("data")]
        public ChannelTeam[] ChannelTeams { get; protected set; }
    }
}