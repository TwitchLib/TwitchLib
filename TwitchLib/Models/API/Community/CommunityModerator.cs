using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Community
{
    /// <summary>Object representing the properties of a community moderator.</summary>
    public class CommunityModerator
    {
        /// <summary>Customizable display name of moderator.</summary>
        public string DisplayName { get; protected set; }
        /// <summary>ID of the moderator.</summary>
        public string Id { get; protected set; }
        /// <summary>Name of moderator.</summary>
        public string Name { get; protected set; }
        /// <summary>Moderator user type.</summary>
        public string Type { get; protected set; }
        /// <summary>Bio of the moderator, if it exists.</summary>
        public string Bio { get; protected set; }
        /// <summary>DateTime object of when the account was created.</summary>
        public DateTime CreatedAt { get; protected set; }
        /// <summary>DateTIme object of the last update of the account.</summary>
        public DateTime UpdatedAt { get; protected set; }
        /// <summary>URL to user's logo.</summary>
        public string Logo { get; protected set; }

        /// <summary>Constructor for CommunityModerator object.</summary>
        /// <param name="json"></param>
        public CommunityModerator(JToken json)
        {
            DisplayName = json.SelectToken("display_name")?.ToString();
            Id = json.SelectToken("_id")?.ToString();
            Name = json.SelectToken("name")?.ToString();
            Type = json.SelectToken("type")?.ToString();
            Bio = json.SelectToken("bio")?.ToString();
            CreatedAt = Common.Helpers.DateTimeStringToObject(json.SelectToken("created_at").ToString());
            UpdatedAt = Common.Helpers.DateTimeStringToObject(json.SelectToken("updated_at").ToString());
            Logo = json.SelectToken("logo")?.ToString();
        }
    }
}
