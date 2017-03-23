using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v4.Clips
{
    public class Broadcaster
    {
        public string ChannelUrl { get; protected set; }
        public string DisplayName { get; protected set; }
        public string Id { get; protected set; }
        public string Logo { get; protected set; }
        public string Name { get; protected set; }

        public Broadcaster(JToken json)
        {

        }
    }
}
