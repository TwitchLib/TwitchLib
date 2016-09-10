using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.TwitchAPIClasses
{
    /// <summary>Class representing a follower as fetched via Twitch API</summary>
    public class Follower
    {
        /// <summary>Property representing whether notifications are enabled or not.</summary>
        public bool Notifications { get; protected set; }
        /// <summary>Property representing date time of follow.</summary>
        public string CreatedAt { get; protected set; }
        /// <summary>Property representing the follower user.</summary>
        public User User { get; protected set; }

        /// <summary>Follower object constructor.</summary>
        public Follower(JToken followerData)
        {
            CreatedAt = followerData.SelectToken("created_at").ToString();
            if (followerData.SelectToken("notifications").ToString() == "true")
                Notifications = true;
            User = new User(followerData.SelectToken("user").ToString());
        }
    }
}