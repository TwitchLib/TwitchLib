using Newtonsoft.Json;

namespace TwitchLib.Models.API.v3.Games
{
    public class Game
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; protected set; }
        [JsonProperty(PropertyName = "box")]
        public Box Box { get; protected set; }
        [JsonProperty(PropertyName = "logo")]
        public Logo Logo { get; protected set; }
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "giantbomb_id")]
        public int GiantBombId { get; protected set; }
    }
}
