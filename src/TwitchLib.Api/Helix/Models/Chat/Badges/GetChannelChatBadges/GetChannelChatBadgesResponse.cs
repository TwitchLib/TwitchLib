using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Chat.Badges.GetChannelChatBadges
{
    public class GetChannelChatBadgesResponse
    {
        [JsonPropertyName("data")]
        public BadgeEmoteSet[] EmoteSet { get; protected set; }
    }
}
