using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.TwitchAPIClasses
{
    /// <summary>Class representing a User object returned from Twitch API.</summary>
    public class User
    {
        /// <summary>Display name of user (CAN BE NULL, USE NAME)</summary>
        public string DisplayName { get; protected set; }
        /// <summary>Twitch Id of user.</summary>
        public int? Id { get; protected set; }
        /// <summary>Username of user.</summary>
        public string Name { get; protected set; }
        /// <summary>Type of user assigned by Twitch.</summary>
        public string Type { get; protected set; }
        /// <summary>Bio of user.</summary>
        public string Bio { get; protected set; }
        /// <summary>Date and time user was created at.</summary>
        public string CreatedAt { get; protected set; }
        /// <summary>Date and time user was last updated (logged in generally)</summary>
        public string UpdatedAt { get; protected set; }
        /// <summary>Link to logo of user.</summary>
        public string Logo { get; protected set; }

        /// <summary>Constructor for User object.</summary>
        public User(string jsonStr)
        {
            JObject json = JObject.Parse(jsonStr);
            DisplayName = json.SelectToken("display_name")?.ToString();
            Id = int.Parse(json.SelectToken("_id")?.ToString());
            Name = json.SelectToken("name")?.ToString();
            Type = json.SelectToken("type")?.ToString();
            Bio = json.SelectToken("bio")?.ToString();
            CreatedAt = json.SelectToken("created_at")?.ToString();
            UpdatedAt = json.SelectToken("updated_at")?.ToString();
            Logo = json.SelectToken("logo")?.ToString();
        }
    }
}
