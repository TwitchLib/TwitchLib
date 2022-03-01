using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Undocumented.Comments
{
    public class UserBadges
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }
        [JsonPropertyName("version")]
        public string Version { get; set; }
    }
}