namespace TwitchLib.Models.API.Undocumented.ChannelPanels
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    public class Data
    {
        [JsonProperty(PropertyName = "link")]
        public string Link { get; protected set; }
        [JsonProperty(PropertyName = "image")]
        public string Image { get; protected set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; protected set; }
    }
}
