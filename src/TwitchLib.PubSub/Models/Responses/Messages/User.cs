using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.PubSub.Models.Responses.Messages
{
    public class User
    {
        [JsonPropertyName("id")]
        public string Id { get; protected set; }
        [JsonPropertyName("login")]
        public string Login { get; protected set; }
        [JsonPropertyName("display_name")]
        public string DisplayName { get; protected set; }
    }
}
