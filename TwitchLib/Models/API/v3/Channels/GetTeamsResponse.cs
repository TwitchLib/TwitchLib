using Newtonsoft.Json;

namespace TwitchLib.Models.API.v3.Channels
{
    public class GetTeamsResponse
    {
        [JsonProperty(PropertyName = "teams")]
        public Teams.Team[] Teams;
    }
}
