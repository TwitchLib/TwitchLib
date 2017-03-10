using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Other.Validate
{
    /// <summary>Class represneting a token from Twitch.</summary>
    public class Token
    {
        /// <summary>Whether or not the token returned is valid.</summary>
        public bool Valid { get; protected set; }
        /// <summary>Details of an auth token if passed (CAN BE NULL)</summary>
        public Authorization Authorization { get; protected set; }
        /// <summary>Username of Twitch account Token is for.</summary>
        public string Username { get; protected set; }
        /// <summary>UserId of the Twitch account.</summary>
        public string UserId { get; protected set; }
        /// <summary>Client ID used to access Twitch API.</summary>
        public string ClientId { get; protected set; }

        /// <summary>Constructor for Token model.</summary>
        /// <param name="json"></param>
        public Token(JToken json)
        {
            bool isValid;
            Valid = bool.TryParse(json.SelectToken("valid").ToString(), out isValid) && isValid;
            if (json.SelectToken("authorization") != null)
                Authorization = new Authorization(json.SelectToken("authorization"));
            Username = json.SelectToken("user_name")?.ToString();
            UserId = json.SelectToken("user_id")?.ToString();
            ClientId = json.SelectToken("client_id")?.ToString();
        }
    }
}
