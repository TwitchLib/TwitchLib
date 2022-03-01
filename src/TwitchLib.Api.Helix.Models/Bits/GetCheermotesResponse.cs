using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Bits
{
    public class GetCheermotesResponse
    {
        [JsonPropertyName("data")]
        public Cheermote[] Listings { get; protected set; }
    }
}
