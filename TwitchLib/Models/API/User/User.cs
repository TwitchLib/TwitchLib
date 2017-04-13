using System;
using Newtonsoft.Json.Linq;

namespace TwitchLib.Models.API.User
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
        public DateTime CreatedAt { get; protected set; }
        /// <summary>TimeSpan object that represents time since the user was created.</summary>
        public TimeSpan TimeSinceCreated { get; protected set; }
        /// <summary>Date and time user was last updated (logged in generally)</summary>
        public DateTime UpdatedAt { get; protected set; }
        /// <summary>TimeSpan object representing the amount of time since the User was last updated.</summary>
        public TimeSpan TimeSinceUpdated { get; protected set; }
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
            CreatedAt = Common.Helpers.DateTimeStringToObject(json.SelectToken("created_at")?.ToString());
            TimeSinceCreated = DateTime.UtcNow - CreatedAt;
            UpdatedAt = Common.Helpers.DateTimeStringToObject(json.SelectToken("updated_at")?.ToString());
            TimeSinceUpdated = DateTime.UtcNow - UpdatedAt;
            Logo = json.SelectToken("logo")?.ToString();
        }
    }
}
