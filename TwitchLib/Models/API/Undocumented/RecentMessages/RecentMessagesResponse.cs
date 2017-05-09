namespace TwitchLib.Models.API.Undocumented.RecentMessages
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    public class RecentMessagesResponse
    {
        [JsonProperty(PropertyName = "messages")]
        public string[] Messages { get; protected set; }
    }
}
