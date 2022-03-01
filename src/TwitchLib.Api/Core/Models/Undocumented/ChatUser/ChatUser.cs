using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Core.Models.Undocumented.ChatUser
{
    public class ChatUser
    {
        [JsonPropertyName("id")]
        public string Id { get; protected set; }
        [JsonPropertyName("version")]
        public string Version { get; protected set; }
    }
}
