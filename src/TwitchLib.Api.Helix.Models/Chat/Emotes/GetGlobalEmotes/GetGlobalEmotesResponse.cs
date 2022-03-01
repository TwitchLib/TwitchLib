using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Chat.Emotes.GetGlobalEmotes
{
    public class GetGlobalEmotesResponse
    {
        [JsonPropertyName("data")]
        public GlobalEmote[] GlobalEmotes { get; protected set; }
    }
}