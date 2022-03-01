using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Chat.Emotes.GetChannelEmotes
{
    public class GetChannelEmotesResponse
    {
        [JsonPropertyName("data")]
        public ChannelEmote[] ChannelEmotes { get; protected set; }
    }
}