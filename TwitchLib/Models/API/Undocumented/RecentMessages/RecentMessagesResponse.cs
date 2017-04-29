using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Undocumented.RecentMessages
{
    public class RecentMessagesResponse
    {
        [JsonProperty(PropertyName = "messages")]
        public string[] Messages { get; protected set; }
    }
}
