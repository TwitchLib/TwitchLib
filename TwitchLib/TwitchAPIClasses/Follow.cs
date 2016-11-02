using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.TwitchAPIClasses
{
    /// <summary>Object representing a follow between a user/viewer and a channel/streamer.</summary>
    public class Follow
    {
        /// <summary>Bool representing if user follows channel. If false, all other properties are null.</summary>
        public bool IsFollowing { get; protected set; }
        /// <summary>DateTime object representing when a follow was created.</summary>
        public DateTime CreatedAt { get; protected set; }
        /// <summary>TimeSpan object representing the amount of time since the follow was created.</summary>
        public TimeSpan TimeSinceCreated { get; protected set; }
        /// <summary>Bool representing whether or not the user receives notificaitons for their follow.</summary>
        public bool Notifications { get; protected set; }
        /// <summary>Channel details returned along with the request.</summary>
        public Channel Channel { get; protected set; }

        /// <summary>Constructor for follow</summary>
        public Follow(string apiResponse, bool successful = true)
        {
            IsFollowing = successful;
            if(successful)
            {
                JObject json = JObject.Parse(apiResponse);
                CreatedAt = Convert.ToDateTime(json.SelectToken("created_at").ToString());
                TimeSinceCreated = DateTime.UtcNow - CreatedAt;
                if ((bool)json.SelectToken("notifications"))
                    Notifications = true;
                Channel = new Channel(json.SelectToken("channel"));
            }
        }
    }
}
