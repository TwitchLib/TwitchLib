using System;
using Newtonsoft.Json.Linq;

namespace TwitchLib
{
    public class TwitchSubscription
    {
        public string Id { get; }
        public string CreatedAt { get; }
        public TwitchUser User { get; }

        public TwitchSubscription(JObject json)
        {
            CreatedAt = json.SelectToken("created_at")?.ToString();
            Id = json.SelectToken("_id")?.ToString();
            User = new TwitchUser(json.SelectToken("user"));
        }
    }
}