﻿namespace TwitchLib.Models.PubSub.Responses.Messages
{
    #region using directives
    using Newtonsoft.Json.Linq;
    #endregion
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
        /// <summary>Next badge at value</summary>
        public int NewBadgeEntitlement { get; protected set; }
        /// <summary>Previous badge received at value</summary>
        public int PreviousBadgeEntitlement { get; protected set; }

        /// <summary>ChannelBitsEvent model constructor.</summary>
        public ChannelBitsEvents(string jsonStr)
        {


            /*
                    JObject message = JObject.Parse((String)json["data"]["message"]);

                    String username = (String)message["data"]["user_name"];
                    String chat_message = (String)message["data"]["chat_message"];
                    int bits = (Int32)message["data"]["bits_used"];

                    // Then, if bits > bitsThreshold, create an event.
                    String displayMessage = String.Format("{0} donated {1} bits.", username, bits);
             */

            JToken json = JObject.Parse(jsonStr);
            Username = json.SelectToken("data").SelectToken("user_name")?.ToString();
            ChannelName = json.SelectToken("data").SelectToken("channel_name")?.ToString();
            UserId = json.SelectToken("data").SelectToken("user_id")?.ToString();
            ChannelId = json.SelectToken("data").SelectToken("channel_id")?.ToString();
            Time = json.SelectToken("data").SelectToken("time")?.ToString();
            ChatMessage = json.SelectToken("data").SelectToken("chat_message")?.ToString();
            BitsUsed = int.Parse(json.SelectToken("data").SelectToken("bits_used").ToString());
            TotalBitsUsed = int.Parse(json.SelectToken("data").SelectToken("total_bits_used").ToString());
            Context = json.SelectToken("data").SelectToken("context")?.ToString();
            NewBadgeEntitlement = int.Parse(json.SelectToken("data").SelectToken("badge_entitlement").SelectToken("new_version").ToString());
            PreviousBadgeEntitlement = int.Parse(json.SelectToken("data").SelectToken("badge_entitlement").SelectToken("previous_version").ToString());
        }
    }
}
