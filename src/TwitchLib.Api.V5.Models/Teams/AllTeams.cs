using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.Teams
{
    public class AllTeams
    {
        #region Teams
        [JsonProperty(PropertyName = "teams")]
        public Team[] Teams { get; protected set; }
        #endregion
    }
}
