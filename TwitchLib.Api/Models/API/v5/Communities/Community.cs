using Newtonsoft.Json;

namespace TwitchLib.Models.API.v5.Communities
{
    public class Community
    {
        #region Id
        /// <summary>Property representing the community ID.</summary>
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; protected set; }
        #endregion
        #region AvatarImageUrl
        /// <summary>Property representing the community avatar image.</summary>
        [JsonProperty(PropertyName = "avatar_image_url")]
        public string AvatarImageUrl { get; protected set; }
        #endregion
        #region CoverImageUrl
        /// <summary>Property representing the community cover image.</summary>
        [JsonProperty(PropertyName = "cover_image_url")]
        public string CoverImageUrl { get; protected set; }
        #endregion
        #region Description
        /// <summary>Property representing the community description.</summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; protected set; }
        #endregion
        #region DescriptionHtml
        /// <summary>Property representing the community description in html format.</summary>
        [JsonProperty(PropertyName = "description_html")]
        public string DescriptionHtml { get; protected set; }
        #endregion
        #region Language
        /// <summary>Property representing the community language.</summary>
        [JsonProperty(PropertyName = "language")]
        public string Language { get; protected set; }
        #endregion
        #region Name
        /// <summary>Property representing the community name.</summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; protected set; }
        #endregion
        #region OwnerId
        /// <summary>Property representing the userId of the community owner.</summary>
        [JsonProperty(PropertyName = "owner_id")]
        public string OwnerId { get; protected set; }
        #endregion
        #region Rules
        /// <summary>Property representing the community rules.</summary>
        [JsonProperty(PropertyName = "rules")]
        public string Rules { get; protected set; }
        #endregion
        #region RulesHtml
        /// <summary>Property representing the community rules in html format.</summary>
        [JsonProperty(PropertyName = "rules_html")]
        public string RulesHtml { get; protected set; }
        #endregion
        #region Summary
        /// <summary>Property representing the community summary.</summary>
        [JsonProperty(PropertyName = "summary")]
        public string Summary { get; protected set; }
        #endregion
    }
}
