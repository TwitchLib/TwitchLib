using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.Badges
{
    public class BadgeContent
    {
        #region Image_Url_1x
        [JsonProperty(PropertyName = "image_url_1x")]
        public string Image_Url_1x { get; protected set; }
        #endregion
        #region Image_Url_2x
        [JsonProperty(PropertyName = "image_url_2x")]
        public string Image_Url_2x { get; protected set; }
        #endregion
        #region Image_Url_4x
        [JsonProperty(PropertyName = "image_url_4x")]
        public string Image_Url_4x { get; protected set; }
        #endregion
        #region Description
        [JsonProperty(PropertyName = "description")]
        public string Description { get; protected set; }
        #endregion
        #region Title
        [JsonProperty(PropertyName = "title")]
        public string Title { get; protected set; }
        #endregion
        #region ClickAction
        [JsonProperty(PropertyName = "click_action")]
        public string ClickAction { get; protected set; }
        #endregion
        #region ClickUrl
        [JsonProperty(PropertyName = "click_url")]
        public string ClickUrl { get; protected set; }
        #endregion
    }
}
