using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v5.Communities
{
    public class TopCommunity
    {
        #region AvatarImageUrl
        [JsonProperty(PropertyName = "avatar_image_url")]
        public string AvatarImageUrl { get; protected set; }
        #endregion
        #region Channels
        [JsonProperty(PropertyName = "channels")]
        public int Channels { get; protected set; }
        #endregion
        #region Id
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        #endregion
        #region Name
        [JsonProperty(PropertyName = "name")]
        public string Name { get; protected set; }
        #endregion
        #region Viewers
        [JsonProperty(PropertyName = "viewers")]
        public int Viewers { get; protected set; }
        #endregion
    }
}
