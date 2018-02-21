using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v3.Teams
{
    public class GetTeamsResponse
    {
        [JsonProperty(PropertyName = "teams")]
        public Team[] Teams { get; protected set; }
    }
}
