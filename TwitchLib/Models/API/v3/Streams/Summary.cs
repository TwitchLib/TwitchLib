using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Streams
{
    public class Summary
    {
        public int Viewers { get; protected set; }
        public int Channels { get; protected set; }

        public Summary(JToken json)
        {

        }
    }
}
