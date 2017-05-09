namespace TwitchLib.Models.API.v5.Communities
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    public class BannedUser
    {
        #region UserId
        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; protected set; }
        #endregion
        #region DisplayName
        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; protected set; }
        #endregion
        #region Name
        [JsonProperty(PropertyName = "name")]
        public string Name { get; protected set; }
        #endregion
        #region Bio
        [JsonProperty(PropertyName = "bio")]
        public string Bio { get; protected set; }
        #endregion
        #region AvatarImageUrl
        [JsonProperty(PropertyName = "avatar_image_url")]
        public string AvatarImageUrl { get; protected set; }
        #endregion
        #region StartTimestamp
        [JsonProperty(PropertyName = "start_timestamp")]
        public long StartTimestamp { get; protected set; }
        #endregion
    }
}
