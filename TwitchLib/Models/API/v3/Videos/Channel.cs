using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Videos
{
    public class Channel
    {
        public string Name { get; protected set; }
        public string DisplayName { get; protected set; }

        public Channel(JToken json)
        {

        }
    }
}
