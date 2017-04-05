using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.ThirdParty
{
    public class UsernameChangeListing
    {

        public string UserId { get; protected set; }
        public string UsernameOld { get; protected set; }
        public string UsernameNew { get; protected set; }
        public DateTime FoundAt { get; protected set; }

        public UsernameChangeListing(JToken json)
        {
            UserId = json.SelectToken("userid")?.ToString();
            UsernameOld = json.SelectToken("username_old")?.ToString();
            UsernameNew = json.SelectToken("username_new")?.ToString();
            FoundAt = Common.Helpers.DateTimeStringToObject(json.SelectToken("found_at").ToString());
        }
    }
}
