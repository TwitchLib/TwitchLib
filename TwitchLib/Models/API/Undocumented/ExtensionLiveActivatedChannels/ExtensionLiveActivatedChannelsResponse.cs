using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Models.API.Undocumented.ExtensionLiveActivatedChannels
{
    public class ExtensionLiveActivatedChannelsResponse
    {
        [JsonProperty(PropertyName = "channels")]
        public Channel[] Channels { get; protected set; }
    }
}
