using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Chat.Emotes
{
    public abstract class Emote
    {
        [JsonPropertyName("id")]
        public string Id { get; protected set; }
        [JsonPropertyName("name")]
        public string Name { get; protected set; }
        [JsonPropertyName("images")]
        public EmoteImages Images { get; protected set; }
    }
}