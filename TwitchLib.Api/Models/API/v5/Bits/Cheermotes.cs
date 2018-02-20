using Newtonsoft.Json;

namespace TwitchLib.Models.API.v5.Bits
{
    public class Cheermotes
    {
        [JsonProperty(PropertyName = "actions")]
        public Action[] Actions { get; protected set; }
    }
}
