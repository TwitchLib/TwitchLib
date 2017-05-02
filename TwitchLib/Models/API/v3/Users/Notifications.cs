using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Users
{
    public class Notifications
    {
        [JsonProperty(PropertyName = "email")]
        public bool Email { get; protected set; }
        [JsonProperty(PropertyName = "push")]
        public bool Push { get; protected set; }
    }
}
