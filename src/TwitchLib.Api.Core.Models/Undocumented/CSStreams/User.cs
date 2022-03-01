using System.Text.Json.Serialization;
using System;

namespace TwitchLib.Api.Core.Models.Undocumented.CSStreams
{
    public class User
    {
        [JsonPropertyName("mature")]
        public bool Mature { get; protected set; }
        [JsonPropertyName("status")]
        public string Status { get; protected set; }
        [JsonPropertyName("broadcaster_language")]
        public string BroadcasterLanguage { get; protected set; }
        [JsonPropertyName("display_name")]
        public string DisplayName { get; protected set; }
        [JsonPropertyName("game")]
        public string Game { get; protected set; }
        [JsonPropertyName("localized_game")]
        public LocalizedGame LocalizedGame { get; protected set; }
        [JsonPropertyName("_id")]
        public string Id { get; protected set; }
        [JsonPropertyName("name")]
        public string Name { get; protected set; }
        [JsonPropertyName("bio")]
        public string Bio { get; protected set; }
        [JsonPropertyName("partner")]
        public bool Partner { get; protected set; }
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; protected set; }
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; protected set; }
        [JsonPropertyName("delay")]
        public string Delay { get; protected set; }
        [JsonPropertyName("prerolls")]
        public bool Prerolls { get; protected set; }
        [JsonPropertyName("postrolls")]
        public bool Postrolls { get; protected set; }
        [JsonPropertyName("primary_team_name")]
        public string PrimaryTeamName { get; protected set; }
        [JsonPropertyName("primary_team_display_name")]
        public string PrimaryTeamDisplayName { get; protected set; }
        [JsonPropertyName("logo")]
        public string Logo { get; protected set; }
        [JsonPropertyName("banner")]
        public string Banner { get; protected set; }
        [JsonPropertyName("video_banner")]
        public string VideoBanner { get; protected set; }
        [JsonPropertyName("background")]
        public string Background { get; protected set; }
        [JsonPropertyName("profile_banner")]
        public string ProfileBanner { get; protected set; }
        [JsonPropertyName("profile_banner_background_color")]
        public string ProfileBannerBackgroundColor { get; protected set; }
        [JsonPropertyName("url")]
        public string Url { get; protected set; }
        [JsonPropertyName("views")]
        public int Views { get; protected set; }
        [JsonPropertyName("followers")]
        public int Followers { get; protected set; }
    }
}
