using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Teams
{
    public class GetTeamsResponse
    {
        [JsonProperty(PropertyName = "teams")]
        public Team[] Teams { get; protected set; }
    }
}
