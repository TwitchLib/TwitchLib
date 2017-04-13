using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace TwitchLib.Models.API.Community
{
    /// <summary></summary>
    public class CommunityBannedUsersResponse
    {
        /// <summary>String used to identify where the results came from.</summary>
        public string Cursor { get; protected set; }
        /// <summary></summary>
        public List<CommunityUser> BannedUsers { get; protected set; } = new List<CommunityUser>();

        /// <summary>CommunityBannedUsersResponse constructor.</summary>
        /// <param name="json"></param>
        public CommunityBannedUsersResponse(JToken json)
        {
            Cursor = json.SelectToken("_cursor")?.ToString();
            if (json.SelectToken("banned_users") != null)
                foreach (JToken bannedUser in json.SelectToken("banned_users"))
                    BannedUsers.Add(new CommunityUser(bannedUser));
        }
    }
}
