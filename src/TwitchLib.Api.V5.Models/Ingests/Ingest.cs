using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.Ingests
{
    public class Ingest
    {
        #region Id
        [JsonProperty(PropertyName = "_id")]
        public int Id { get; protected set; }
        #endregion
        #region Availability
        [JsonProperty(PropertyName = "availability")]
        public double Availability { get; protected set; }
        #endregion
        #region Default
        [JsonProperty(PropertyName = "default")]
        public bool Default { get; protected set; }
        #endregion
        #region Name
        [JsonProperty(PropertyName = "name")]
        public string Name { get; protected set; }
        #endregion
        #region UrlTemplate
        [JsonProperty(PropertyName = "url_template")]
        public string UrlTemplate { get; protected set; }
        #endregion
    }
}
