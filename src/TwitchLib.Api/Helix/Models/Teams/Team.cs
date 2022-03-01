using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Teams
{
    public class Team : TeamBase
    {
        [JsonPropertyName("users")]
        public TeamMember[] Users { get; protected set; }
    }
}