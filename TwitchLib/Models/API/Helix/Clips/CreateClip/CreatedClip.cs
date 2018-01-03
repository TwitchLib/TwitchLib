using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Models.API.Helix.Clips.CreateClip
{
    public class CreatedClip
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "edit_url")]
        public string EditUrl { get; protected set; }
    }
}
