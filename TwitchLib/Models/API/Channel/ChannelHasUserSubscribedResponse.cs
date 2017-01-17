using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.Models.API.Channel
{
    /// <summary>Class representing a response from Twitch API for ChannelUserHasSubscribed</summary>
    public class ChannelHasUserSubscribedResponse
    {
        /// <summary>Property representing internal variable Id</summary>
        public string Id { get; protected set; }
        /// <summary>Property representing a User object.</summary>
        public Models.API.User.User User { get; protected set; }
        /// <summary>Property representing the created at datetime object.</summary>
        public DateTime CreatedAt { get; protected set; }
        
        /// <summary>Constructor for ChannelHasUserSubscribedResponse object.</summary>
        /// <param name="json"></param>
        public ChannelHasUserSubscribedResponse(JToken json)
        {
            Id = json.SelectToken("_id")?.ToString();
            if (json.SelectToken("user") != null)
                User = new Models.API.User.User(json.SelectToken("user").ToString());
            if (json.SelectToken("created_at") != null)
                CreatedAt = Common.Helpers.DateTimeStringToObject(json.SelectToken("created_at").ToString());
        }
    }
}
