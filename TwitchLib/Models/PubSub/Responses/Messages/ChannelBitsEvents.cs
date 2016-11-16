using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.Models.PubSub.Responses.Messages
{
    public class ChannelBitsEvents : MessageData
    {
        public string Username { get; protected set; }
        public string ChannelName { get; protected set; }
        public string UserId { get; protected set; }
        public string ChannelId { get; protected set; }
        public string Time { get; protected set; }
        public string ChatMessage { get; protected set; }
        public int BitsUsed { get; protected set; }
        public int TotalBitsUsed { get; protected set; }
        public string Context { get; protected set; }

        public ChannelBitsEvents(string jsonStr)
        {
            JToken json = JObject.Parse(jsonStr);
            Username = json.SelectToken("user_name")?.ToString();
            ChannelName = json.SelectToken("channel_name")?.ToString();
            UserId = json.SelectToken("user_id")?.ToString();
            ChannelId = json.SelectToken("channel_id")?.ToString();
            Time = json.SelectToken("time")?.ToString();
            ChatMessage = json.SelectToken("chat_message")?.ToString();
            BitsUsed = int.Parse(json.SelectToken("bits_used").ToString());
            TotalBitsUsed = int.Parse(json.SelectToken("total_bits_used").ToString());
            Context = json.SelectToken("context")?.ToString();
        }
    }
}
