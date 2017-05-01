using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Subscriber
{
    /// <summary>Class representing a channel subscription as fetched via Twitch API</summary>
    public class Subscriptionv5
    {
        /// <summary>DateTime object representing when a subscription was created.</summary>
        public DateTime CreatedAt { get; protected set; }
        /// <summary>TimeSpan object representing the amount of time since the subscription was created.</summary>
        public TimeSpan TimeSinceCreated { get; protected set; }

        public Enums.SubscriptionPlan SubscriptionPlan { get; protected set; }

        public string SubscriptionPlanName { get; protected set; }
        /// <summary>User details returned along with the request.</summary>
        public Models.API.User.User User { get; protected set; }

        /// <summary>Constructor for Subscription</summary>
        public Subscriptionv5(string apiResponse)
        {
            JObject json = JObject.Parse(apiResponse);
            CreatedAt = Convert.ToDateTime(json.SelectToken("created_at").ToString());
            TimeSinceCreated = DateTime.UtcNow - CreatedAt;
            switch(json.SelectToken("sub_plan").ToString().ToLower())
            {
                case "prime":
                    SubscriptionPlan = Enums.SubscriptionPlan.Prime;
                    break;
                case "1000":
                    SubscriptionPlan = Enums.SubscriptionPlan.Tier1;
                    break;
                case "2000":
                    SubscriptionPlan = Enums.SubscriptionPlan.Tier2;
                    break;
                case "3000":
                    SubscriptionPlan = Enums.SubscriptionPlan.Tier3;
                    break;
            }
            SubscriptionPlanName = json.SelectToken("sub_plan_name")?.ToString();
            User = new Models.API.User.User(json.SelectToken("user").ToString());
        }

        /// <summary>Constructor for Subscription (using JToken as param)</summary>
        /// <param name="json"></param>
        public Subscriptionv5(JToken json)
        {
            CreatedAt = Convert.ToDateTime(json.SelectToken("created_at").ToString());
            TimeSinceCreated = DateTime.UtcNow - CreatedAt;
            switch (json.SelectToken("sub_plan").ToString().ToLower())
            {
                case "prime":
                    SubscriptionPlan = Enums.SubscriptionPlan.Prime;
                    break;
                case "1000":
                    SubscriptionPlan = Enums.SubscriptionPlan.Tier1;
                    break;
                case "2000":
                    SubscriptionPlan = Enums.SubscriptionPlan.Tier2;
                    break;
                case "3000":
                    SubscriptionPlan = Enums.SubscriptionPlan.Tier3;
                    break;
            }
            SubscriptionPlanName = json.SelectToken("sub_plan_name")?.ToString();
            User = new Models.API.User.User(json.SelectToken("user").ToString());
        }
    }
}
