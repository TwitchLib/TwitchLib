using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Helix.StreamsMetadata
{
    public class StreamMetadata
    {
        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; protected set; }
        [JsonProperty(PropertyName = "game_id")]
        public string GameId { get; protected set; }
        [JsonProperty(PropertyName = "hearthstone")]
        public Hearthstone Hearthstone { get; protected set; }
        [JsonProperty(PropertyName = "overwatch")]
        public Overwatch Overwatch { get; protected set; }
    }
}
