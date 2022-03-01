using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.Videos
{
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
