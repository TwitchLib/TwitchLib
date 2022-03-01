using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Chat.Emotes.GetEmoteSets
{
    public class GetEmoteSetsResponse
    {
        [JsonPropertyName("data")]
        public EmoteSet[] EmoteSets { get; protected set; }
    }
}