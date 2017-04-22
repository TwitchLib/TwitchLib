using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Videos
{
    public class TopVideosResponse
    {
        [JsonProperty(PropertyName = "videos")]
        public Video[] TopVideos { get; protected set; }
    }
}
