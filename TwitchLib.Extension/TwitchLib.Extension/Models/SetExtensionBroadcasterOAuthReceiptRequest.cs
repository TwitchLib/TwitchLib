using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Extension.Models
{
    public class SetExtensionBroadcasterOAuthReceiptRequest
    {
        [JsonProperty(PropertyName = "permissions_received")]
        public bool Permissions_Received { get; internal set; }
    }
}
