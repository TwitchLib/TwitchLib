using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.Bits
{
    public class Cheermotes
    {
        [JsonProperty(PropertyName = "actions")]
        public Action[] Actions { get; protected set; }
    }
}
