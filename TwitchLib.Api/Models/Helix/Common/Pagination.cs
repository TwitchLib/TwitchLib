using Newtonsoft.Json;

namespace TwitchLib.Api.Models.Helix.Common
{
    public class Pagination
    {
        [JsonProperty(PropertyName = "cursor")]
        public string Cursor { get; protected set; }
    }
}
