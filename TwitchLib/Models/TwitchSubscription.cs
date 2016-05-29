using System;
using Newtonsoft.Json.Linq;

namespace TwitchLib
{
    public class TwitchSubscription
    {
        public string Id { get; set; }
        public string CreatedAt { get; set; }
        public TwitchUser User { get; set; }

        public TwitchSubscription(JObject subscriptionData)
        {
            CreatedAt = subscriptionData.SelectToken("created_at").ToString();
            Id = subscriptionData.SelectToken("_id").ToString();
            User = new TwitchUser(subscriptionData.SelectToken("user"));
        }
    }
}