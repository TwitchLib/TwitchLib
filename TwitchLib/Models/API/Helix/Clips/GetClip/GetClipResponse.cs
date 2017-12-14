using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Models.API.Helix.Clips.GetClip
{
    public class GetClipResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Clip[] Clips { get; protected set; }
    }
}
