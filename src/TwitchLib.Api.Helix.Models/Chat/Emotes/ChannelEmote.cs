using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Chat.Emotes
{
    public class ChannelEmote : Emote
    {
        [JsonPropertyName("tier")]
        public string Tier { get; protected set; }
        [JsonPropertyName("emote_type")]
        public string EmoteType { get; protected set; }
        [JsonPropertyName("emote_set_id")]
        public string EmoteSetId { get; protected set; }
    }
}