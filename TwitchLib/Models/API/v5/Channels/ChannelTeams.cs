namespace TwitchLib.Models.API.v5.Channels
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    /// <summary>Class representing the teams response of a channel from Twitch API.</summary>
    public class ChannelTeams
    {
        #region Teams
        /// <summary>Property representing the channel teams.</summary>
        [JsonProperty(PropertyName = "teams")]
        public Teams.Team[] Teams { get; protected set; }
        #endregion
    }
}
