using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Follows
{
    public class FollowersResponse
    {
        public int Total { get; protected set; }
        public string Cursor { get; protected set; }
        public List<Follow> Follows { get; protected set; }

        public FollowersResponse(JToken json)
        {

        }
    }
}
