using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.APIv5.Models
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
        public long Id { get; protected set; }
        /// <summary>Property representing the bio of the user.</summary>
        public string Bio { get; protected set; }

        /// <summary>User constructor.</summary>
        /// <param name="json"></param>
        public User(JToken json)
        {
            Type = json.SelectToken("type")?.ToString();
            Name = json.SelectToken("name")?.ToString();
            CreatedAt = Common.DateTimeStringToObject(json.SelectToken("created_at")?.ToString());
            UpdatedAt = Common.DateTimeStringToObject(json.SelectToken("updated_at")?.ToString());
            Logo = json.SelectToken("logo")?.ToString();
            Id = (json.SelectToken("_id") != null) ? long.Parse(json.SelectToken("_id").ToString()) : -1;
            Bio = json.SelectToken("bio")?.ToString();
        }
    }
}
