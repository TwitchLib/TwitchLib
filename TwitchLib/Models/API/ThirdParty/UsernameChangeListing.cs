using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.ThirdParty
{
    public class UsernameChangeListing
    {
        [JsonProperty(PropertyName = "userid")]
        public string UserId { get; protected set; }
        [JsonProperty(PropertyName = "username_old")]
        public string UsernameOld { get; protected set; }
        [JsonProperty(PropertyName = "username_new")]
        public string UsernameNew { get; protected set; }
        [JsonProperty(PropertyName = "found_at")]
        public DateTime FoundAt { get; protected set; }
    }
}
