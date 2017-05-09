namespace TwitchLib.Models.API.Undocumented.ChannelPanels
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    public class Panel
    {
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "display_order")]
        public int DisplayOrder { get; protected set; }
        [JsonProperty(PropertyName = "default")]
        public string Kind { get; protected set; }
        [JsonProperty(PropertyName = "html_description")]
        public string HtmlDescription { get; protected set; }
        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; protected set; }
        [JsonProperty(PropertyName = "data")]
        public Data Data { get; protected set; }
        [JsonProperty(PropertyName = "channel")]
        public string Channel { get; protected set; }
    }
}
