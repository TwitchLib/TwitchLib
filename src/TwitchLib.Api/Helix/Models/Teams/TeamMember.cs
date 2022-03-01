using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Teams
{
    public class TeamMember
    {
        [JsonPropertyName("user_id")]
        public string UserId { get; protected set; }
        [JsonPropertyName("user_name")]
        public string UserName { get; protected set; }
        [JsonPropertyName("user_login")]
        public string UserLogin { get; protected set; }
    }
}