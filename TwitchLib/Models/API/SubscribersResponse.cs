using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace TwitchLib.Models.API
{
    /// <summary>Class representing response from Twitch API for channel Subscribers.</summary>
    public class SubscribersResponse
    {
        /// <summary>Property representing list of Subscriber objects.</summary>
        public List<Subscription> Subscribers { get; protected set; } = new List<Subscription>();
        /// <summary>Property representing total subscriber count.</summary>
        public int TotalSubscriberCount { get; protected set; }
        
        /// <summary>SubscribersResponse constructor.</summary>
        /// <param name="json"></param>
        public SubscribersResponse(JToken json)
        {
            if (json.SelectToken("subscriptions") != null)
                foreach (JToken sub in json.SelectToken("subscriptions"))
                    Subscribers.Add(new Subscription(sub));
            if (json.SelectToken("_total") != null)
                TotalSubscriberCount = int.Parse(json.SelectToken("_total").ToString());
        }

        /// <summary>
        /// SubscribersResponse constructor (with subs and total subs as params)
        /// </summary>
        /// <param name="subscribers"></param>
        /// <param name="totalSubs"></param>
        public SubscribersResponse(List<Subscription> subscribers, int totalSubs)
        {
            Subscribers = subscribers;
            TotalSubscriberCount = totalSubs;
        }
    }
}