using Newtonsoft.Json;
using System;

namespace TwitchLib.Api.Models.v3.Subscriptions
{
    public class Subscriber
    {
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "user")]
        public Users.User User { get; protected set; }
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; protected set; }
    }
}
