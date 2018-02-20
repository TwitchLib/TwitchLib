using Newtonsoft.Json;

namespace TwitchLib.Models.API.v3.Teams
{
    public class GetTeamsResponse
    {
        [JsonProperty(PropertyName = "teams")]
        public Team[] Teams { get; protected set; }
    }
}
