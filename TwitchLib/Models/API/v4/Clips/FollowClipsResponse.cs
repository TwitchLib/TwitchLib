using Newtonsoft.Json;
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
        [JsonProperty(PropertyName = "_cursor")]
        public string Cursor { get; protected set; }
        [JsonProperty(PropertyName = "clips")]
        public Clip[] Clips { get; protected set; }
    }
}
