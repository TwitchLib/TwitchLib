namespace TwitchLib.Models.API.Undocumented.Chatters
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    public class ChattersResponse
    {
        [JsonProperty(PropertyName = "chatter_count")]
        public int ChatterCount { get; protected set; }
        [JsonProperty(PropertyName = "chatters")]
        public Chatters Chatters { get; protected set; }
    }
}
