using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v5.Streams
{
    public class FeaturedStream
    {
        #region Image
        [JsonProperty(PropertyName = "image")]
        public string Image { get; protected set; }
        #endregion
        #region Priority
        [JsonProperty(PropertyName = "priority")]
        public int Priority { get; protected set; }
        #endregion
        #region Scheduled
        [JsonProperty(PropertyName = "scheduled")]
        public bool Scheduled { get; protected set; }
        #endregion
        #region Sponsored
        [JsonProperty(PropertyName = "sponsored")]
        public bool Sponsored { get; protected set; }
        #endregion
        #region Stream
        [JsonProperty(PropertyName = "stream")]
        public Stream Stream { get; protected set; }
        #endregion
        #region Text
        [JsonProperty(PropertyName = "text")]
        public string Text { get; protected set; }
        #endregion
        #region Title
        [JsonProperty(PropertyName = "title")]
        public string Title { get; protected set; }
        #endregion
    }
}
