using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Channels
{
    public class GetEditorsResponse
    {
        [JsonProperty(PropertyName = "users")]
        public Users.User[] Editors { get; protected set; }
    }
}
