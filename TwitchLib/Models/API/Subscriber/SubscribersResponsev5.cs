using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Subscriber
{
    public class SubscribersResponsev5
    {
        /// <summary>Property representing list of Subscriber objects.</summary>
        public List<Subscriptionv5> Subscribers { get; protected set; } = new List<Subscriptionv5>();
        /// <summary>Property representing total subscriber count.</summary>
        public int TotalSubscriberCount { get; protected set; }

        /// <summary>SubscribersResponse constructor.</summary>
        /// <param name="json"></param>
        public SubscribersResponsev5(JToken json)
        {
            if (json.SelectToken("subscriptions") != null)
                foreach (JToken sub in json.SelectToken("subscriptions"))
                    Subscribers.Add(new Subscriptionv5(sub));
            if (json.SelectToken("_total") != null)
                TotalSubscriberCount = int.Parse(json.SelectToken("_total").ToString());
        }

        /// <summary>
        /// SubscribersResponse constructor (with subs and total subs as params)
        /// </summary>
        /// <param name="subscribers"></param>
        /// <param name="totalSubs"></param>
        public SubscribersResponsev5(List<Subscriptionv5> subscribers, int totalSubs)
        {
            Subscribers = subscribers;
            TotalSubscriberCount = totalSubs;
        }
    }
}
