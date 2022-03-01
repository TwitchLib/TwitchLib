using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Chat.Badges.GetGlobalChatBadges
{
    public class GetGlobalChatBadgesResponse
    {
        [JsonPropertyName("data")]
        public BadgeEmoteSet[] EmoteSet { get; protected set; }
    }
}
