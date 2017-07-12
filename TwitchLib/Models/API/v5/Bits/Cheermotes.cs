using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v5.Bits
{
    public class Cheermotes
    {
        [JsonProperty(PropertyName = "actions")]
        public Action[] Actions { get; protected set; }
    }
}
