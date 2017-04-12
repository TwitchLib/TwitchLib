using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Chat
{
    public class AllEmoticonsResponse
    {
        [JsonProperty(PropertyName = "emoticons")]
        public Emoticon[] Emoticons { get; protected set; }
    }
}
