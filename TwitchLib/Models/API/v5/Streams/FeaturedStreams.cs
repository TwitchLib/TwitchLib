namespace TwitchLib.Models.API.v5.Streams
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    public class FeaturedStreams
    {
        #region Featured
        [JsonProperty(PropertyName = "featured")]
        public FeaturedStream[] Featured { get; protected set; }
        #endregion
    }
}
