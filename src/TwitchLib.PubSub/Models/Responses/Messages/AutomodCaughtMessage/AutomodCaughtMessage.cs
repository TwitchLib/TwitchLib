using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.PubSub.Models.Responses.Messages.AutomodCaughtMessage
{
    /// <summary>
    /// Model representing the data in automod caught message
    /// Implements the <see cref="MessageData" />
    /// </summary>
    /// <seealso cref="MessageData" />
    /// <inheritdoc />
    public class AutomodCaughtMessage : AutomodQueueData
    {
        [JsonPropertyName("content_classification")]
        public ContentClassification ContentClassification { get; protected set; }
        [JsonPropertyName("message")]
        public Message Message { get; protected set; }
        [JsonPropertyName("reason_code")]
        public string ReasonCode { get; protected set; }
        [JsonPropertyName("resolver_id")]
        public string ResolverId { get; protected set; }
        [JsonPropertyName("resolver_login")]
        public string ResolverLogin { get; protected set; }
        [JsonPropertyName("status")]
        public string Status { get; protected set; }

    }
}
