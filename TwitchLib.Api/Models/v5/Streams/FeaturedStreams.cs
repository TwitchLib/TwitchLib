using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v5.Streams
{
    public class FeaturedStreams
    {
        #region Featured
        [JsonProperty(PropertyName = "featured")]
        public FeaturedStream[] Featured { get; protected set; }
        #endregion
    }
}
