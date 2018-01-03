using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Extension.Models
{
    public class ExtensionPubSubRequest
    {
        [JsonProperty(PropertyName = "content_type")]
        public string Content_Type { get; protected set; }
        [JsonProperty(PropertyName = "message")]
        public object Message { get; protected set; }
        [JsonProperty(PropertyName = "targets")]
        public string[] Targets { get; protected set; }
    }
}
