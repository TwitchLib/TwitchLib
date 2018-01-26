using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Extension.Models
{
    public class Secret
    {
        public Secret()
        {

        }

        public Secret(string content, DateTime active, DateTime expires)
        {
            Content = content;
            Active = active;
            Expires = expires;

        }
        [JsonProperty(PropertyName = "active")]
        public DateTime Active { get; protected set; }
        [JsonProperty(PropertyName = "content")]
        public string Content { get; protected set; }
        [JsonProperty(PropertyName = "expires")]
        public DateTime Expires { get; protected set; }
    }
}
