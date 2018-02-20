using Newtonsoft.Json;

namespace TwitchLib.Api.Models.Undocumented.CSStreams
{
    public class LocalizedGame
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; protected set; }
        [JsonProperty(PropertyName = "popularity")]
        public int Popularity { get; protected set; }
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "giantbomb_id")]
        public string GiantbombId { get; protected set; }
        [JsonProperty(PropertyName = "box")]
        public Box Box { get; protected set; }
        [JsonProperty(PropertyName = "logo")]
        public Logo Logo { get; protected set; }
        [JsonProperty(PropertyName = "localized_name")]
        public string LocalizedName { get; protected set; }
        [JsonProperty(PropertyName = "locale")]
        public string Locale { get; protected set; }
    }
}
