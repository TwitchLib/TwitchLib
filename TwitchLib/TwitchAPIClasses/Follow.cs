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
        private bool _isFollowing;
        private DateTime _createdAt;
        private bool _notifications;
        private TwitchChannel _channel;

        /// <summary>Bool representing if user follows channel. If false, all other properties are null.</summary>
        public bool IsFollowing => _isFollowing;
        /// <summary>DateTime object representing when a follow was created.</summary>
        public DateTime CreatedAt => _createdAt;
        /// <summary>Bool representing whether or not the user receives notificaitons for their follow.</summary>
        public bool Notifications => _notifications;
        /// <summary>Channel details returned along with the request.</summary>
        public TwitchChannel Channel => _channel;

        /// <summary>Constructor for follow</summary>
        public Follow(string apiResponse, bool successful = true)
        {
            _isFollowing = successful;
            if(successful)
            {
                JObject json = JObject.Parse(apiResponse);
                _createdAt = Convert.ToDateTime(json.SelectToken("created_at").ToString());
                if ((bool)json.SelectToken("notifications"))
                    _notifications = true;
                _channel = new TwitchChannel(json.SelectToken("channel"));
            }
        }
    }
}
