﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Community
{
    /// <summary>
    /// Object representing a banned community member.
    /// </summary>
    public class CommunityBannedUser
    {
        /// <summary>Twitch assigned unique ID of users.</summary>
        public string UserId { get; protected set; }
        /// <summary>Customizable display name of user.</summary>
        public string DisplayName { get; protected set; }
        /// <summary>Name of user.</summary>
        public string Name { get; protected set; }
        /// <summary>Bio of the user</summary>
        public string Bio { get; protected set; }
        /// <summary>Url to image of user's avatar.</summary>
        public string AvatarImageUrl { get; protected set; }
        /// <summary>Start time stamp for ban.</summary>
        public long StartTimestamp { get; protected set; }

        /// <summary></summary>
        public CommunityBannedUser(JToken json)
        {
            UserId = json.SelectToken("user_id")?.ToString();
            DisplayName = json.SelectToken("display_name")?.ToString();
            Name = json.SelectToken("name")?.ToString();
            Bio = json.SelectToken("bio")?.ToString();
            AvatarImageUrl = json.SelectToken("avatar_image_url")?.ToString();
            if (json.SelectToken("start_timestamp") != null)
                StartTimestamp = long.Parse(json.SelectToken("start_timestamp").ToString());
        }
    }
}
