using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Channels.GetChannelEditors
{
    public class ChannelEditor
    {
        [JsonPropertyName("user_id")]
        public string UserId { get; protected set; }
        [JsonPropertyName("user_name")]
        public string UserName { get; protected set; }
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; protected set; }
    }
}
