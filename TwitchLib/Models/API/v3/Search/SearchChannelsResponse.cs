using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Search
{
    public class SearchChannelsResponse
    {
        public List<Channels.Channel> Channels { get; protected set; }
        public int Total { get; protected set; }

        public SearchChannelsResponse(JToken json)
        {

        }
    }
}
