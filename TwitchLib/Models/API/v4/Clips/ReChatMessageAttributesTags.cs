using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v4.Clips
{
    public class ReChatMessageAttributesTags
    {
        [JsonProperty(PropertyName = "badges")]
        public string Badges { get; protected set; }
        [JsonProperty(PropertyName = "color")]
        public string Color { get; protected set; }
        [JsonProperty(PropertyName = "display-name")]
        public string DisplayName { get; protected set; }
        [JsonProperty(PropertyName = "emotes")]
        public Dictionary<string, int[][]> Emotes { get; protected set; }
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "mod")]
        public bool Mod { get; protected set; }
        [JsonProperty(PropertyName = "room-id")]
        public string RoomId { get; protected set; }
        [JsonProperty(PropertyName = "sent-ts")]
        public string SentTs { get; protected set; }
        [JsonProperty(PropertyName = "subscriber")]
        public bool Subscriber { get; protected set; }
        [JsonProperty(PropertyName = "tmi-sent-ts")]
        public string TmiSentTs { get; protected set; }
        [JsonProperty(PropertyName = "turbo")]
        public bool Turbo { get; protected set; }
        [JsonProperty(PropertyName = "user-id")]
        public string UserId { get; protected set; }
        [JsonProperty(PropertyName = "user-type")]
        public string UserType { get; protected set; }
    }
}
