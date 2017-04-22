namespace TwitchLib.Models.API.v5.Streams
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    public class StreamPreview
    {
        #region Large
        /// <summary>Property representing the large preview image of the stream.</summary>
        [JsonProperty(PropertyName = "large")]
        public string Large { get; protected set; }
        #endregion
        #region Medium
        /// <summary>Property representing the medium preview image of the stream.</summary>
        [JsonProperty(PropertyName = "medium")]
        public string Medium { get; protected set; }
        #endregion
        #region Small
        /// <summary>Property representing the small preview image of the stream.</summary>
        [JsonProperty(PropertyName = "small")]
        public string Small { get; protected set; }
        #endregion
        #region Template
        /// <summary>Property representing the template preview image of the stream.</summary>
        [JsonProperty(PropertyName = "template")]
        public string Template { get; protected set; }
        #endregion
    }
}
