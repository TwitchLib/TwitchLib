using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v4.Clips
{
    public class FollowClipsResponse
    {
        public string Cursor { get; protected set; }
        public List<Clip> Clips { get; protected set; }

        public FollowClipsResponse(JToken json)
        {

        }

    }
}
