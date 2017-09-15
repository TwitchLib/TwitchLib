using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Helix.Users.GetUsersFollows
{
    public class Pagination
    {
        [JsonProperty(PropertyName = "cursor")]
        public string Cursor { get; protected set; }
    }
}
