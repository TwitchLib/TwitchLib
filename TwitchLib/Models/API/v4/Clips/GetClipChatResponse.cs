using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v4.Clips
{
    public class GetClipChatResponse
    {
        [JsonProperty(PropertyName = "data")]
        public ReChatMessage[] Messages { get; protected set; }
    }
}
