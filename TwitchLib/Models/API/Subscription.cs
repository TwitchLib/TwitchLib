using System;
using Newtonsoft.Json.Linq;

namespace TwitchLib.Models.API
{
    /// <summary>Class representing a channel subscription as fetched via Twitch API</summary>
    public class Subscription
    {
        /// <summary>DateTime object representing when a subscription was created.</summary>
        public DateTime CreatedAt { get; protected set; }
        /// <summary>TimeSpan object representing the amount of time since the subscription was created.</summary>
        public TimeSpan TimeSinceCreated { get; protected set; }
        /// <summary>User details returned along with the request.</summary>
        public User User { get; protected set; }

        /// <summary>Constructor for Subscription</summary>
        public Subscription(string apiResponse)
        {
            JObject json = JObject.Parse(apiResponse);
            CreatedAt = Convert.ToDateTime(json.SelectToken("created_at").ToString());
            TimeSinceCreated = DateTime.UtcNow - CreatedAt;
            User = new User(json.SelectToken("user").ToString());
        }

        /// <summary>Constructor for Subscription (using JToken as param)</summary>
        /// <param name="json"></param>
        public Subscription(JToken json)
        {
            CreatedAt = Convert.ToDateTime(json.SelectToken("created_at").ToString());
            TimeSinceCreated = DateTime.UtcNow - CreatedAt;
            User = new User(json.SelectToken("user").ToString());
        }
    }
}
