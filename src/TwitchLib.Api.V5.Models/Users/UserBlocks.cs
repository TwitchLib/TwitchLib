using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.Users
{
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
