using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Subscriptions
{
    public class SubscriptionsResponse
    {
        public int Total { get; protected set; }
        public List<Subscription> Subscriptions = new List<Subscription>();

        public SubscriptionsResponse(JToken json)
        {

        }
    }
}
