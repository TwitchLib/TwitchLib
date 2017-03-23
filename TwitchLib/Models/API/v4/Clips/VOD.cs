using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v4.Clips
{
    public class VOD
    {
        public string Id { get; protected set; }
        public string Url { get; protected set; }

        public VOD(JToken json)
        {

        }
    }
}
