﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Community
{
    /// <summary></summary>
    public class CommunityBannedUsersResponse
    {
        /// <summary>String used to identify where the results came from.</summary>
        public string Cursor { get; protected set; }
        /// <summary></summary>
        public List<CommunityBannedUser> BannedUsers { get; protected set; } = new List<CommunityBannedUser>();

        /// <summary>CommunityBannedUsersResponse constructor.</summary>
        /// <param name="json"></param>
        public CommunityBannedUsersResponse(JToken json)
        {
            Cursor = json.SelectToken("_cursor")?.ToString();
            if (json.SelectToken("banned_users") != null)
                foreach (JToken bannedUser in json.SelectToken("banned_users"))
                    BannedUsers.Add(new CommunityBannedUser(bannedUser));
        }
    }
}
