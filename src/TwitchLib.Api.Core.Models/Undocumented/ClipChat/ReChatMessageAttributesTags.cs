using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Undocumented.ClipChat
{
    public class ReChatMessageAttributesTags
    {
        [JsonPropertyName("badges")]
        public string Badges { get; protected set; }
        [JsonPropertyName("color")]
        public string Color { get; protected set; }
        [JsonPropertyName("display-name")]
        public string DisplayName { get; protected set; }
        [JsonPropertyName("emotes")]
        public Dictionary<string, int[][]> Emotes { get; protected set; }
        [JsonPropertyName("id")]
        public string Id { get; protected set; }
        [JsonPropertyName("mod")]
        public bool Mod { get; protected set; }
        [JsonPropertyName("room-id")]
        public string RoomId { get; protected set; }
        [JsonPropertyName("sent-ts")]
        public string SentTs { get; protected set; }
        [JsonPropertyName("subscriber")]
        public bool Subscriber { get; protected set; }
        [JsonPropertyName("tmi-sent-ts")]
        public string TmiSentTs { get; protected set; }
        [JsonPropertyName("turbo")]
        public bool Turbo { get; protected set; }
        [JsonPropertyName("user-id")]
        public string UserId { get; protected set; }
        [JsonPropertyName("user-type")]
        public string UserType { get; protected set; }
    }
}
