using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.TwitchAPIClasses
{
    public class FollowersResponse
    {
        public List<Follower> Followers { get; protected set; } = new List<Follower>();
        public int TotalFollowerCount { get; protected set; }
        public string Cursor { get; protected set; }
        public FollowersResponse(string apiResponse)
        {
            JObject json = JObject.Parse(apiResponse);
            foreach (JToken follower in json.SelectToken("follows"))
                Followers.Add(new Follower(follower));
            TotalFollowerCount = int.Parse(json.SelectToken("_total").ToString());
            Cursor = json.SelectToken("_cursor").ToString();
        }
    }
}
