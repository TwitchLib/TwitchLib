using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Subscriptions
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
