using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Community
{
    /// <summary>Object representing a response from fetching timedout users.</summary>
    public class CommunityTimedOutUsersResponse
    {
        /// <summary>String used to identify where the results came from.</summary>
        public string Cursor { get; protected set; }
        /// <summary>List of timed out users in the community</summary>
        public List<CommunityUser> TimedOutUsers { get; protected set; } = new List<CommunityUser>();

        /// <summary>CommunityTimedOutUsersResponse constructor.</summary>
        /// <param name="json"></param>
        public CommunityTimedOutUsersResponse(JToken json)
        {
            Cursor = json.SelectToken("_cursor")?.ToString();
            if (json.SelectToken("timed_out_users") != null)
                foreach (JToken timedoutUser in json.SelectToken("timed_out_users"))
                    TimedOutUsers.Add(new CommunityUser(timedoutUser));
        }
    }
}
