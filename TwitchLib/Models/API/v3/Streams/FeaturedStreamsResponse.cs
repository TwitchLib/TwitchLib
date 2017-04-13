using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Streams
{
    public class FeaturedStreamsResponse
    {
        [JsonProperty(PropertyName = "featured")]
        public FeaturedStream[] FeaturedStreams { get; protected set; }
    }
}
