using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.PubSub.Models.Responses.Messages.AutomodCaughtMessage
{
    public class Message
    {
        [JsonPropertyName("content")]
        public Content Content;
        [JsonPropertyName("id")]
        public string Id;
        [JsonPropertyName("sender")]
        public Sender Sender;
        [JsonPropertyName("sent_at")]
        public DateTime SentAt;
    }
}
