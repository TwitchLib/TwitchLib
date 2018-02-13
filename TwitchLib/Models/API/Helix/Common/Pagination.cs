using Newtonsoft.Json;

namespace TwitchLib.Models.API.Helix.Common
{
    public class Pagination
    {
        [JsonProperty(PropertyName = "cursor")]
        public string Cursor { get; protected set; }
    }
}
