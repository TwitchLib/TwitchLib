namespace TwitchLib.Models.API.v5.Videos
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    public class VideoChannel
    {
        #region Id
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; protected set; }
        #endregion
        #region DisplayName
        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; protected set; }
        #endregion
        #region Name
        [JsonProperty(PropertyName = "name")]
        public string Name { get; protected set; }
        #endregion
    }
}
