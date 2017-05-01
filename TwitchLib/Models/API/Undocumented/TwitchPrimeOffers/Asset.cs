namespace TwitchLib.Models.API.Undocumented.TwitchPrimeOffers
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    public class Asset
    {
        [JsonProperty(PropertyName = "assetType")]
        public string AssetType { get; protected set; }
        [JsonProperty(PropertyName = "location")]
        public string Location { get; protected set; }
        [JsonProperty(PropertyName = "location2x")]
        public string Location2x { get; protected set; }
        [JsonProperty(PropertyName = "mediaType")]
        public string MediaType { get; protected set; }
    }
}
