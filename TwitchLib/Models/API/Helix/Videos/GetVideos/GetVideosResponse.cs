using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Models.API.Helix.Videos.GetVideos
{
    public class GetVideosResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Video[] Videos { get; protected set; }
    }
}
