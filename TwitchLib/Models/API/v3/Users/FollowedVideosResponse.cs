using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Users
{
    public class FollowedVideosResponse
    {
        [JsonProperty(PropertyName = "videos")]
        public Videos.Video[] Videos { get; protected set; } 
    }
}
