using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v3.Channels
{
    public class GetTeamsResponse
    {
        [JsonProperty(PropertyName = "teams")]
        public Teams.Team[] Teams;
    }
}
