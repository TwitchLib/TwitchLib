using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Follows
{
    public class FollowsResponse
    {
        public int Total { get; protected set; }
        public List<Follows> Follows { get; protected set; } = new List<v3.Follows.Follows>();

        public FollowsResponse(JToken json)
        {

        }
    }
}
