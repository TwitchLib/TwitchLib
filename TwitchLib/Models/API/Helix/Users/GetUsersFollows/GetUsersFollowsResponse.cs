using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Helix.Users.GetUsersFollows
{
    public class GetUsersFollowsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Follow[] Follows { get; protected set; }
        [JsonProperty(PropertyName = "pagination")]
        public Pagination Pagination { get; protected set; }
    }
}
