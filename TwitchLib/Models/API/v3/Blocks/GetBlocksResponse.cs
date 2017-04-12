using Newtonsoft.Json;

namespace TwitchLib.Models.API.v3.Blocks
{
    public class GetBlocksResponse
    {
        [JsonProperty(PropertyName = "blocks")]
        public Block[] Blocks { get; protected set; }
    }
}
