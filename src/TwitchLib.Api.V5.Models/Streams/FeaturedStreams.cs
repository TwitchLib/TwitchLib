using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.Streams
{
    public class FeaturedStreams
    {
        #region Featured
        [JsonProperty(PropertyName = "featured")]
        public FeaturedStream[] Featured { get; protected set; }
        #endregion
    }
}
