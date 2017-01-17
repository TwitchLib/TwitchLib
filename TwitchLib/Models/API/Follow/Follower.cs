using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.Models.API.Follow
{
    /// <summary>Class representing a follower as fetched via Twitch API</summary>
    public class Follower
    {
        /// <summary>Property representing whether notifications are enabled or not.</summary>
        public bool Notifications { get; protected set; }
        /// <summary>Property representing date time of follow.</summary>
        public DateTime CreatedAt { get; protected set; }
        /// <summary>Property representing the amount of time since the follow was created.</summary>
        public TimeSpan TimeSinceCreated { get; protected set; }
        /// <summary>Property representing the follower user.</summary>
        public Models.API.User.User User { get; protected set; }

        /// <summary>Follower object constructor.</summary>
        public Follower(JToken followerData)
        {
            CreatedAt = Common.Helpers.DateTimeStringToObject(followerData.SelectToken("created_at").ToString());
            TimeSinceCreated = DateTime.UtcNow - CreatedAt;
            if (followerData.SelectToken("notifications").ToString() == "true")
                Notifications = true;
            User = new User.User(followerData.SelectToken("user").ToString());
        }
    }
}