using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.Models.PubSub.Responses.Messages
{
    /// <summary>Model representing the data in a channel bits event.</summary>
    public class ChannelBitsEvents : MessageData
    {
        /// <summary>Username of the sender.</summary>
        public string Username { get; protected set; }
        /// <summary>The channel the bits were sent to.</summary>
        public string ChannelName { get; protected set; }
        /// <summary>User ID of the sender.</summary>
        public string UserId { get; protected set; }
        /// <summary>Channel/User ID of where the bits were sent to.</summary>
        public string ChannelId { get; protected set; }
        /// <summary>Time stamp of the event.</summary>
        public string Time { get; protected set; }
        /// <summary>Chat message that accompanied the bits.</summary>
        public string ChatMessage { get; protected set; }
        /// <summary>The amount of bits sent.</summary>
        public int BitsUsed { get; protected set; }
        /// <summary>The total amount of bits the user has sent.</summary>
        public int TotalBitsUsed { get; protected set; }
        /// <summary>Context related to event.</summary>
        public string Context { get; protected set; }

        /// <summary>ChannelBitsEvent model constructor.</summary>
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
