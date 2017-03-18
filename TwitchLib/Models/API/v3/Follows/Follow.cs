using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Follows
{
    public class Follow
    {
        public DateTime CreatedAt { get; protected set; }
        public bool Notifications { get; protected set; }
        public User User { get; protected set; }

        public Follow(JToken json)
        {

        }
    }
}
