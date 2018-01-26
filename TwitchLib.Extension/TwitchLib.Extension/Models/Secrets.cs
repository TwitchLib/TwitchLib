using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Extension.Models
{
    public class ExtensionSecrets
    {
        [JsonProperty(PropertyName = "format_version")]
        public string Format_Version { get; protected set; }
        [JsonProperty(PropertyName = "secrets")]
        public Secret[] Secrets { get; protected set; }
    }
}
