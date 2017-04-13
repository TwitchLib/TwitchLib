using System;
using Newtonsoft.Json.Linq;

namespace TwitchLib.Models.API.v5
{
    /// <summary>Class representing a user object from Twitch API.</summary>
    public class User
    {
        /// <summary>Property representing the type of data requested</summary>
        public string Type { get; protected set; }
        /// <summary>Property representing the name of the user requested</summary>
        public string Name { get; protected set; }
        /// <summary>DateTime object representing the creation of the account.</summary>
        public DateTime CreatedAt { get; protected set; }
        /// <summary>DateTime object representing the most recent update of the account.</summary>
        public DateTime UpdatedAt { get; protected set; }
        /// <summary>Property representing the logo of the user.</summary>
        public string Logo { get; protected set; }
        /// <summary>Property representing the Id of the user.</summary>
        public string Id { get; protected set; }
        /// <summary>Property representing the bio of the user.</summary>
        public string Bio { get; protected set; }

        /// <summary>User constructor.</summary>
        /// <param name="json"></param>
        public User(JToken json)
        {
            Type = json.SelectToken("type")?.ToString();
            Name = json.SelectToken("name")?.ToString();
            CreatedAt = Common.Helpers.DateTimeStringToObject(json.SelectToken("created_at")?.ToString());
            UpdatedAt = Common.Helpers.DateTimeStringToObject(json.SelectToken("updated_at")?.ToString());
            Logo = json.SelectToken("logo")?.ToString();
            Id = json.SelectToken("_id")?.ToString();
            Bio = json.SelectToken("bio")?.ToString();
        }
    }
}
