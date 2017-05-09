using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.PubSub.Responses.Messages
{
    /// <summary>ChatModeratorActions model.</summary>
    public class  ChannelSubscription: MessageData
    {
        public string Username { get; protected set; }
        public string DisplayName { get; protected set; }
        public string ChannelName { get; protected set; }
        public string UserId { get; protected set; }
        public string ChannelId { get; protected set; }
        public DateTime Time { get; protected set; }
        public Enums.SubscriptionPlan SubscriptionPlan { get; protected set; }
        public string SubscriptionPlanName { get; protected set; }
        public int Months { get; protected set; }
        public string Context { get; protected set; }
        public SubMessage SubMessage { get; protected set; }

        /// <summary>ChatModeratorActions model constructor.</summary>
        public ChannelSubscription(string jsonStr)
        {
            JToken message = JObject.Parse(jsonStr);
            Username = message.SelectToken("user_name")?.ToString();
            DisplayName = message.SelectToken("display_name")?.ToString();
            ChannelName = message.SelectToken("channel_name")?.ToString();
            UserId = message.SelectToken("user_id")?.ToString();
            ChannelId = message.SelectToken("channel_id")?.ToString();
            Time = Common.Helpers.DateTimeStringToObject(message.SelectToken("time")?.ToString());
            switch(message.SelectToken("sub_plan").ToString().ToLower())
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
            SubscriptionPlanName = message.SelectToken("sub_plan_name")?.ToString();
            Months = int.Parse(message.SelectToken("months").ToString());
            SubMessage = new SubMessage(message.SelectToken("sub_message"));
        }
    }
}
