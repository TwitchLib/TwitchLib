using Newtonsoft.Json;

namespace TwitchLib.Models.API.Undocumented.RecentMessages
{
    public class RecentMessagesResponse
    {
        [JsonProperty(PropertyName = "messages")]
        public string[] Messages { get; protected set; }
    }
}
