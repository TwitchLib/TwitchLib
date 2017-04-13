using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace TwitchLib.Models.API.Other.Validate
{
    /// <summary>Class representing details of a specific access token.</summary>
    public class Authorization
    {
        /// <summary>List of scopes assigned to an access token.</summary>
        public List<string> Scopes { get; protected set; } = new List<string>();
        /// <summary>Creation date of the access token.</summary>
        public DateTime CreatedAt { get; protected set; }
        /// <summary>Last time the token was updated.</summary>
        public DateTime UpdatedAt { get; protected set; }

        /// <summary>Constructor for Authorization object.</summary>
        /// <param name="json"></param>
        public Authorization(JToken json)
        {
            if (json.SelectToken("scopes") != null)
                foreach (JToken scope in json.SelectToken("scopes"))
                    Scopes.Add(scope.ToString());

            if (json.SelectToken("created_at") != null)
                CreatedAt = Common.Helpers.DateTimeStringToObject(json.SelectToken("created_at").ToString());

            if (json.SelectToken("updated_at") != null)
                UpdatedAt = Common.Helpers.DateTimeStringToObject(json.SelectToken("updated_at").ToString());
        }
    }
}
