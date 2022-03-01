using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace TwitchLib.Api.Core.Models.Undocumented.ChatUser
{
    public class ChatUserResponse
    {
        [JsonPropertyName("_id")]
        public string Id { get; protected set; }
        [JsonPropertyName("login")]
        public string Login { get; protected set; }
        [JsonPropertyName("display_name")]
        public string DisplayName { get; protected set; }
        [JsonPropertyName("color")]
        public string Color { get; protected set; }
        [JsonPropertyName("is_verified_bot")]
        public bool IsVerifiedBot { get; protected set; }
        [JsonPropertyName("badges")]
        public List<ChatUser> Badges { get; protected set; }
    }
}
