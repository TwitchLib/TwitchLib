using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v4.Clips
{
    public class Thumbnails
    {
        public string Medium { get; protected set; }
        public string Small { get; protected set; }
        public string Tiny { get; protected set; }

        public Thumbnails(JToken json)
        {

        }
    }
}
