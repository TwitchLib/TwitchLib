namespace TwitchLib.Models.API.v5.Users
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    public class UserBlocks
    {
        #region Total
        [JsonProperty(PropertyName = "_total")]
        public int Total { get; protected set; }
        #endregion
        #region Blocks
        [JsonProperty(PropertyName = "blocks")]
        public UserBlock[] Blocks { get; protected set; }
        #endregion
    }
}
