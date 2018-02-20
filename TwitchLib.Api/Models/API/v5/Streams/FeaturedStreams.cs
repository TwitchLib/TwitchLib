using Newtonsoft.Json;

namespace TwitchLib.Models.API.v5.Streams
{
    public class FeaturedStreams
    {
        #region Featured
        [JsonProperty(PropertyName = "featured")]
        public FeaturedStream[] Featured { get; protected set; }
        #endregion
    }
}
