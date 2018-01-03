using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Models.API.Helix.Clips.CreateClip
{
    public class CreatedClipResponse
    {
        [JsonProperty(PropertyName = "data")]
        public CreatedClip[] CreatedClips { get; protected set; }
    }
}
