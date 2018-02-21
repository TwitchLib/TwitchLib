using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Models.v5.Chat
{
    public class ChatRoomsByChannelResponse
    {
        [JsonProperty(PropertyName = "_total")]
        public int Total { get; protected set; }
        [JsonProperty(PropertyName = "rooms")]
        public ChatRoom[] Rooms { get; protected set; }
    }
}
