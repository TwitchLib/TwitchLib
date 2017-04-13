using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace TwitchLib.Models.API.Follow
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
        
        /// <summary>FollowersResponse object constructor.</summary>
        public FollowersResponse(string apiResponse)
        {
            JObject json = JObject.Parse(apiResponse);
            foreach (JToken follower in json.SelectToken("follows"))
                Followers.Add(new Follower(follower));
            TotalFollowerCount = int.Parse(json.SelectToken("_total").ToString());
            Cursor = json.SelectToken("_cursor")?.ToString();
        }
    }
}
