using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v3.Subscriptions
{
    public class SubscribersResponse
    {
        [JsonProperty(PropertyName = "_total")]
        public int Total { get; protected set; }
        [JsonProperty(PropertyName = "subscriptions")]
        public Subscriber[] Subscribers { get; protected set; }
    }
}
