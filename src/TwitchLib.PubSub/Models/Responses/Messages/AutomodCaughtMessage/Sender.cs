using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.PubSub.Models.Responses.Messages.AutomodCaughtMessage
{
    public class Sender
    {
        [JsonPropertyName("user_id")]
        public string UserId;
        [JsonPropertyName("login")]
        public string Login;
        [JsonPropertyName("display_name")]
        public string DisplayName;
    }
}
