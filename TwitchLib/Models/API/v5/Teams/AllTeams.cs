namespace TwitchLib.Models.API.v5.Teams
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    public class AllTeams
    {
        #region Teams
        [JsonProperty(PropertyName = "teams")]
        public Team[] Teams { get; protected set; }
        #endregion
    }
}
