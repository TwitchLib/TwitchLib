using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Teams
{
    public abstract class TeamBase
    {
        [JsonPropertyName("banner")]
        public string Banner { get; protected set; }
        [JsonPropertyName("background_image_url")]
        public string BackgroundImageUrl { get; protected set; }
        [JsonPropertyName("created_at")]
        public string CreatedAt { get; protected set; }
        [JsonPropertyName("updated_at")]
        public string UpdatedAt { get; protected set; }
        public string Info { get; protected set; }
        [JsonPropertyName("thumbnail_url")]
        public string ThumbnailUrl { get; protected set; }
        [JsonPropertyName("team_name")]
        public string TeamName { get; protected set; }
        [JsonPropertyName("team_display_name")]
        public string TeamDisplayName { get; protected set; }
        [JsonPropertyName("id")]
        public string Id { get; protected set; }
    }
}