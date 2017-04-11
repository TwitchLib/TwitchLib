using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Channels
{
    public class GetTeamsResponse
    {
        [JsonProperty(PropertyName = "teams")]
        public Teams.Team[] Teams;
    }
}
