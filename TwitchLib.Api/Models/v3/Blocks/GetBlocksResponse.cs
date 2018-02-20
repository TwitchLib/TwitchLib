using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v3.Blocks
{
    public class GetBlocksResponse
    {
        [JsonProperty(PropertyName = "blocks")]
        public Block[] Blocks { get; protected set; }
    }
}
