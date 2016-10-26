using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.TwitchAPIClasses
{
    /// <summary>Class representing response from Twitch API for followers.</summary>
    public class FollowersResponse
    {
        /// <summary>Property representing list of Follower objects.</summary>
        public List<Follower> Followers { get; protected set; } = new List<Follower>();
        /// <summary>Property representing total follower count.</summary>
        public int TotalFollowerCount { get; protected set; }
        /// <summary>Property representing cursor for pagination.</summary>
        public string Cursor { get; protected set; }
        /// <summary>This call has a tendency to return random 400 response codes, so this will be set to false if 400 received</summary>
        public bool CallSuccessful { get; protected set; }
        
        /// <summary>FollowersResponse object constructor.</summary>
        public FollowersResponse(string apiResponse = null)
        {
            if(apiResponse == null)
            {
                Followers = new List<Follower>();
                TotalFollowerCount = 0;
                Cursor = "";
                CallSuccessful = false;
                return;
            }
            JObject json = JObject.Parse(apiResponse);
            foreach (JToken follower in json.SelectToken("follows"))
                Followers.Add(new Follower(follower));
            TotalFollowerCount = int.Parse(json.SelectToken("_total").ToString());
            Cursor = json.SelectToken("_cursor").ToString();
            CallSuccessful = true;
        }
    }
}
