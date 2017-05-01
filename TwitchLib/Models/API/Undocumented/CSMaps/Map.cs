namespace TwitchLib.Models.API.Undocumented.CSMaps
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    public class Map
    {
        [JsonProperty(PropertyName = "map")]
        public string MapCode { get; protected set; }
        [JsonProperty(PropertyName = "map_name")]
        public string MapName { get; protected set; }
        [JsonProperty(PropertyName = "map_image")]
        public string MapImage { get; protected set; }
        [JsonProperty(PropertyName = "viewers")]
        public int Viewers { get; protected set; }
    }
}
