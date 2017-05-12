using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Undocumented.CSStreams
{
    public class User
    {
        [JsonProperty(PropertyName = "mature")]
        public bool Mature { get; protected set; }
        [JsonProperty(PropertyName = "status")]
        public string Status { get; protected set; }
        [JsonProperty(PropertyName = "broadcaster_language")]
        public string BroadcasterLanguage { get; protected set; }
        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; protected set; }
        [JsonProperty(PropertyName = "game")]
        public string Game { get; protected set; }
        [JsonProperty(PropertyName = "localized_game")]
        public LocalizedGame LocalizedGame { get; protected set; }
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; protected set; }
        [JsonProperty(PropertyName = "bio")]
        public string Bio { get; protected set; }
        [JsonProperty(PropertyName = "partner")]
        public bool Partner { get; protected set; }
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; protected set; }
        [JsonProperty(PropertyName = "updated_at")]
        public DateTime UpdatedAt { get; protected set; }
        [JsonProperty(PropertyName = "delay")]
        public string Delay { get; protected set; }
        [JsonProperty(PropertyName = "prerolls")]
        public bool Prerolls { get; protected set; }
        [JsonProperty(PropertyName = "postrolls")]
        public bool Postrolls { get; protected set; }
        [JsonProperty(PropertyName = "primary_team_name")]
        public string PrimaryTeamName { get; protected set; }
        [JsonProperty(PropertyName = "primary_team_display_name")]
        public string PrimaryTeamDisplayName { get; protected set; }
        [JsonProperty(PropertyName = "logo")]
        public string Logo { get; protected set; }
        [JsonProperty(PropertyName = "banner")]
        public string Banner { get; protected set; }
        [JsonProperty(PropertyName = "video_banner")]
        public string VideoBanner { get; protected set; }
        [JsonProperty(PropertyName = "background")]
        public string Background { get; protected set; }
        [JsonProperty(PropertyName = "profile_banner")]
        public string ProfileBanner { get; protected set; }
        [JsonProperty(PropertyName = "profile_banner_background_color")]
        public string ProfileBannerBackgroundColor { get; protected set; }
        [JsonProperty(PropertyName = "url")]
        public string Url { get; protected set; }
        [JsonProperty(PropertyName = "views")]
        public int Views { get; protected set; }
        [JsonProperty(PropertyName = "followers")]
        public int Followers { get; protected set; }
    }
}
