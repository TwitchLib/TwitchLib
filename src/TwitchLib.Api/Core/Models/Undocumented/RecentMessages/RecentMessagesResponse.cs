using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Undocumented.RecentMessages
{
    public class RecentMessagesResponse
    {
        [JsonPropertyName("messages")]
        public string[] Messages { get; protected set; }
    }
}
