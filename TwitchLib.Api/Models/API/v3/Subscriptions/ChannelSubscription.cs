using Newtonsoft.Json;
using System;

namespace TwitchLib.Models.API.v3.Subscriptions
{
    public class ChannelSubscription
    {
        [JsonProperty(PropertyName = "channel")]
        public Channels.Channel Channel { get; protected set; }
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; protected set; }
    }
}
