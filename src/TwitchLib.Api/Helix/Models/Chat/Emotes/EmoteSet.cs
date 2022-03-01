using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Chat.Emotes
{
    public class EmoteSet : Emote
    {
        [JsonPropertyName("emote_type")]
        public string EmoteType { get; protected set; }
        [JsonPropertyName("emote_set_id")]
        public string EmoteSetId { get; protected set; }
        [JsonPropertyName("owner_id")]
        public string OwnerId { get; protected set; }
    }
}