using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace TwitchLib.PubSub.Models.Responses.Messages
{
    public class ChannelBitsEventsV2 : MessageData
    {
        [JsonPropertyName("user_name")]
        public string UserName { get; protected set; }
        [JsonPropertyName("channel_name")]
        public string ChannelName { get; protected set; }
        [JsonPropertyName("user_id")]
        public string UserId { get; protected set; }
        [JsonPropertyName("channel_id")]
        public string ChannelId { get; protected set; }
        [JsonPropertyName("time")]
        public DateTime Time { get; protected set; }
        [JsonPropertyName("chat_message")]
        public string ChatMessage { get; protected set; }
        [JsonPropertyName("bits_used")]
        public int BitsUsed { get; protected set; }
        [JsonPropertyName("total_bits_used")]
        public int TotalBitsUsed { get; protected set; }
        [JsonPropertyName("is_anonymous")]
        public bool IsAnonymous { get; protected set; }
        [JsonPropertyName("context")]
        public string Context { get; protected set; }

        public ChannelBitsEventsV2(string jsonStr)
        {
            jsonStr = jsonStr.Replace("\\", "");
            var dataEncoded = JsonDocument.Parse(jsonStr).RootElement.GetProperty("data");
            UserName = dataEncoded.GetProperty("user_name").GetString();
            ChannelName = dataEncoded.GetProperty("channel_name").GetString();
            UserId = dataEncoded.GetProperty("user_id").GetString();
            ChannelId = dataEncoded.GetProperty("channel_id").GetString();
            Time = dataEncoded.GetProperty("time").GetDateTime();
            ChatMessage = dataEncoded.GetProperty("chat_message").GetString();
            BitsUsed = dataEncoded.GetProperty("bits_used").GetInt32();
            TotalBitsUsed = dataEncoded.GetProperty("total_bits_used").GetInt32();
            IsAnonymous = dataEncoded.GetProperty("is_anonymous").GetBoolean();
            Context = dataEncoded.GetProperty("context").GetString();
        }
    }
}
